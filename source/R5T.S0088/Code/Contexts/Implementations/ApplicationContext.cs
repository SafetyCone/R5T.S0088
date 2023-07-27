using System;
using System.Collections.Generic;
using R5T.L0052.T001.N001;
using R5T.T0137;
using R5T.T0162;
using R5T.T0172;
using R5T.T0181;
using R5T.T0212.F000;

namespace R5T.S0088
{
    [ContextImplementationMarker]
    public class ApplicationContext :
        L0038.ApplicationContext
    {
        #region Static

        public static ApplicationContext Constuctor()
        {
            return new ApplicationContext();
        }

        #endregion


        public IInstancesJsonFilePath OutputInstancesFilePath { get; set; }
        public IProjectFilePath[] InstanceProjectFilePaths { get; set; }
        public IProjectFilePath[] RecursiveProjectFilePaths { get; set; }
        public IDictionary<IIdentityName, MemberDocumentation> RawMemberDocumentationsByMemberName { get; set; }
        public IDictionary<IIdentityName, MemberDocumentation> MemberDocumentationsByMemberName { get; set; }
        public MissingDocumentationReference[] MissingDocumentationReferences { get; set; }
    }
}
