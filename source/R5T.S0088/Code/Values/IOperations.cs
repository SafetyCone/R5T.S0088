using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0131;
using R5T.T0179.Extensions;
using R5T.T0181.Extensions;


namespace R5T.S0088
{
    [ValuesMarker]
    public partial interface IOperations : IValuesMarker
    {
        /// <summary>
        /// Writes output files, and opens them in Notepad++.
        /// </summary>
        public Task Write_OutputFilesAndOpen(ApplicationContext context)
        {
            var instanceProjectFilePathsFilePath = Instances.PathOperator.Get_FilePath(
                context.LocalRunSpecificDirectoryPath,
                Instances.FileNames.InstanceProjectFilePaths);

            var recursiveProjectFilesFilePath = Instances.PathOperator.Get_FilePath(
                context.LocalRunSpecificDirectoryPath,
                Instances.FileNames.RecursiveProjectFilePaths);

            var rawMemberDocumentationsFilePath = Instances.PathOperator.Get_FilePath(
                context.LocalRunSpecificDirectoryPath,
                Instances.FileNames.RawMemberDocumentations,
                x => x.ToTextFilePath());

            var memberDocumentationsFilePath = Instances.PathOperator.Get_FilePath(
                context.LocalRunSpecificDirectoryPath,
                Instances.FileNames.MemberDocumentations,
                x => x.ToTextFilePath());

            var missingDocumentationReferencesFilePath = Instances.PathOperator.Get_FilePath(
                context.LocalRunSpecificDirectoryPath,
                Instances.FileNames.MissingDocumentationReferences,
                x => x.ToTextFilePath());


            Instances.FileOperator.WriteAllLines_Synchronous(
                instanceProjectFilePathsFilePath.Value,
                context.InstanceProjectFilePaths.Get_Values().OrderAlphabetically());

            Instances.FileOperator.WriteAllLines_Synchronous(
                recursiveProjectFilesFilePath.Value,
                context.RecursiveProjectFilePaths.Get_Values().OrderAlphabetically());

            Instances.MemberDocumentationOperator.Describe_ToFile_Synchronous(
                rawMemberDocumentationsFilePath,
                context.RawMemberDocumentationsByMemberName);

            Instances.MemberDocumentationOperator.Describe_ToFile_Synchronous(
                memberDocumentationsFilePath,
                context.MemberDocumentationsByMemberName);

            Instances.MissingDocumentationReferenceOperator.Describe_ToFile_Synchronous(
                missingDocumentationReferencesFilePath,
                context.MissingDocumentationReferences);

            Instances.NotepadPlusPlusOperator.Open(
                instanceProjectFilePathsFilePath.Value,
                recursiveProjectFilesFilePath.Value,
                rawMemberDocumentationsFilePath.Value,
                memberDocumentationsFilePath.Value,
                missingDocumentationReferencesFilePath.Value);

            return Task.CompletedTask;
        }
    }
}
