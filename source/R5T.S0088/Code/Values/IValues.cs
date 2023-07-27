using System;

using R5T.T0131;
using R5T.T0175; /// <see cref="R5T.T0175.Documentation"/>
using R5T.T0175.Extensions;


namespace R5T.S0088
{
    [ValuesMarker]
    public partial interface IValues : IValuesMarker
    {
        public IApplicationName ApplicationName => "R5T.S0088".ToApplicationName();
    }
}
