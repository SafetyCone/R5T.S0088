using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.L0052.T001.N001.Extensions;
using R5T.T0162;
using R5T.T0172;
using R5T.T0173.Extensions;
using R5T.T0179.Extensions;


namespace R5T.S0088
{
    class Program
    {
        /// <summary>
        /// Post-process the instances file to expand all inheritdoc elements.
        /// </summary>
        static async Task Main()
        {
            var (humanOutputTextFilePath, logFilePath) = await Instances.ApplicationContextOperator.In_ApplicationContext(
                Instances.Values.ApplicationName,
                ApplicationContext.Constuctor,
                async context =>
                {
                    // Setup context.
                    context.OutputInstancesFilePath = Instances.PathOperator.Get_FilePath(
                        context.LocalRunSpecificDirectoryPath,
                        Instances.FileNames.Instances,
                        x => x.ToInstancesJsonFilePath());

                    // Run.
                    context.TextOutput.WriteInformation("Loading instances file...");

                    await Instances.InstancesOperator.In_InstancesContext(
                        async instances =>
                        {
                            // Get the list of all project file paths.
                            context.TextOutput.WriteInformation("Getting instance project file paths...");

                            context.InstanceProjectFilePaths = Instances.AssemblyInstancesDescriptorOperator.Get_ProjectFilePaths(instances);

                            context.TextOutput.WriteInformation("Getting recursive project file paths...");

                            context.RecursiveProjectFilePaths = await Instances.ProjectFileOperations.Get_RecursiveProjectReferences_Inclusive(
                                context.TextOutput,
                                context.InstanceProjectFilePaths);

                            context.TextOutput.WriteInformation("Getting raw documentation comments (recursive)...");

                            var missingDocumentationFileProjectFilePaths = new List<IProjectFilePath>();

                            context.RawMemberDocumentationsByMemberName = await Instances.DocumentationCommentOperations.Get_DocumentationComments_Recursive_Raw(
                                missingDocumentationFileProjectFilePaths,
                                context.TextOutput,
                                // Use the /publish directory path.
                                x => x.ToDictionary(
                                    x => x,
                                    x => Instances.Operations_F0115.CreateProjectFilesTuple(
                                        x,
                                        context.TextOutput).DocumentationFilePath),
                                // Ok to use instance project file paths, since the recursive code will just go find all the recursive project file references again.
                                context.InstanceProjectFilePaths);

                            var missingDocumentationFileProjectsFilePath = Instances.PathOperator.Get_FilePath(
                                context.LocalRunSpecificDirectoryPath,
                                Instances.FileNames.MissingDocumentationFileProjectFilePaths);

                            Instances.FileOperator.WriteLines_Synchronous(
                                missingDocumentationFileProjectsFilePath.Value,
                                missingDocumentationFileProjectFilePaths.Get_Values().OrderAlphabetically());

                            context.TextOutput.WriteInformation("Replacing <inheritdoc> elements...");

                            context.MemberDocumentationsByMemberName = Instances.DocumentationCommentOperations.Expand_InheritdocElements(
                                context.RawMemberDocumentationsByMemberName,
                                context.TextOutput,
                                out var missingDocumentationReferences);
                            context.MissingDocumentationReferences = missingDocumentationReferences;

                            // Update instances.
                            // Keep track of which instances have non-empty description XML values.
                            var missingDocumentations = new List<IIdentityName>();

                            foreach (var instance in instances.Instances)
                            {
                                if(instance.DescriptionXml.Value is not null)
                                {
                                    if(context.MemberDocumentationsByMemberName.TryGetValue(
                                        instance.IdentityName,
                                        out var memberDocumentation))
                                    {
                                        instance.DescriptionXml = Instances.MemberElementOperator.ToString(
                                            memberDocumentation.MemberElement)
                                        .ToDescriptionXml();
                                    }
                                    else
                                    {
                                        missingDocumentations.Add(instance.IdentityName);
                                    }
                                }
                            }

                            var missingDocumentationNamesFilePath = Instances.PathOperator.Get_FilePath(
                                context.LocalRunSpecificDirectoryPath,
                                Instances.FileNames.MissingDocumentationNames);

                            Instances.FileOperator.WriteLines_Synchronous(
                                missingDocumentationNamesFilePath.Value,
                                missingDocumentations.Get_Values());

                            // Output instances to a different file location.
                            Instances.InstancesFileOperator.Save_Instances_Synchronous(
                                instances,
                                context.OutputInstancesFilePath);

                            Instances.NotepadPlusPlusOperator.Open(
                                missingDocumentationFileProjectsFilePath.Value,
                                missingDocumentationNamesFilePath.Value);
                        });

                    context.TextOutput.WriteInformation("Writing output files...");

                    await Instances.Operations.Write_OutputFilesAndOpen(context);

                    Instances.NotepadPlusPlusOperator.Open(
                        context.OutputInstancesFilePath.Value);
                });

            Instances.NotepadPlusPlusOperator.Open(
                humanOutputTextFilePath.Value,
                logFilePath.Value);
        }
    }
}