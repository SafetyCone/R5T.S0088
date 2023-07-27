using System;


namespace R5T.S0088
{
    public static class Instances
    {
        public static L0038.IApplicationContextOperator ApplicationContextOperator => L0038.ApplicationContextOperator.Instance;
        public static F0109.IAssemblyInstancesDescriptorOperator AssemblyInstancesDescriptorOperator => F0109.AssemblyInstancesDescriptorOperator.Instance;
        public static O0027.IDocumentationCommentOperations DocumentationCommentOperations => O0027.DocumentationCommentOperations.Instance;
        public static IFileNames FileNames => S0088.FileNames.Instance;
        public static F0000.IFileOperator FileOperator => F0000.FileOperator.Instance;
        public static L0052.IInstancesFileOperator InstancesFileOperator => L0052.InstancesFileOperator.Instance;
        public static L0052.IInstancesOperator InstancesOperator => L0052.InstancesOperator.Instance;
        public static T0212.F000.IMemberDocumentationOperator MemberDocumentationOperator => T0212.F000.MemberDocumentationOperator.Instance;
        public static T0212.F000.IMissingDocumentationReferenceOperator MissingDocumentationReferenceOperator => T0212.F000.MissingDocumentationReferenceOperator.Instance;
        public static T0212.F000.IMemberElementOperator MemberElementOperator => T0212.F000.MemberElementOperator.Instance;
        public static F0033.INotepadPlusPlusOperator NotepadPlusPlusOperator => F0033.NotepadPlusPlusOperator.Instance;
        public static IOperations Operations => S0088.Operations.Instance;
        public static F0115.IOperations Operations_F0115 => F0115.Operations.Instance;
        public static F0124.IPathOperator PathOperator => F0124.PathOperator.Instance;
        public static O0006.IProjectFileOperations ProjectFileOperations => O0006.ProjectFileOperations.Instance;
        public static IValues Values => S0088.Values.Instance;
    }
}