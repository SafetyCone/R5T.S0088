using System;

using R5T.T0131;
using R5T.T0180;
using R5T.T0180.Extensions;
using R5T.T0181;


namespace R5T.S0088
{
    [ValuesMarker]
    public partial interface IFileNames : IValuesMarker
    {
        public IJsonFileName Instances => L0052.FileNames.Instance.Instances;

        public IFileName InstanceProjectFilePaths => "Projects-Instances.txt".ToFileName();
        public IFileName RecursiveProjectFilePaths => "Projects-Recursive.txt".ToFileName();
        public IFileName RawMemberDocumentations => "Member Documentations-Raw.txt".ToFileName();
        public IFileName MemberDocumentations => "Member Documentations.txt".ToFileName();
        public IFileName MissingDocumentationReferences => "Missing Documentation References.txt".ToFileName();
        public IFileName MissingDocumentationNames => "Missing Documentation Names".ToFileName();
        public IFileName MissingDocumentationFileProjectFilePaths => "Projects-Missing Documentation Files".ToFileName();
    }
}
