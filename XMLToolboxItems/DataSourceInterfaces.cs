using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace XMLFormEditor
{
    /// <summary>
    /// This interface is used by TextBox and Label components
    /// </summary>
    public interface ISingleDataSource : IDataSourceBase
    {
        string getDocumentName();
        string getXPathExpression();
    }
    /// <summary>
    /// This interface is used by Static label components
    /// </summary>
    public interface IStaticLabelDataSource : IDataSourceBase
    {
        string getCaption();        
    }


    /// <summary>
    /// This interface is used by List components
    /// </summary>
    public interface IListDataSource: IDataSourceBase
    {
        string getDocumentName();
        string getXPathExpression();
        string getListDocumentName();
        string getListXPathExpression();
        string getListCaptionXPathExpression();
        string getListValueXPathExpression();
    }

    /// <summary>
    /// This interface is used by Button components
    /// </summary>
    public interface IButtonDataSource : IDataSourceBase
    {
        string getDocumentName();
        string getXPathExpression();
        string getInsertText();
    }

    /// <summary>
    /// This interface is used by Pager components
    /// </summary>
    public interface IPagerDataSource : IDataSourceBase
    {
        string getDocumentName();
        string getXPathExpression();
        string getPagerDocumentName();
        string getPagerXPathExpression();
    }

    /// <summary>
    /// This interface is used by Pager components
    /// </summary>
    public interface ISchemaControlDataSource : IDataSourceBase
    {
        string getDocumentName();
        string getSchemaName();
        string getXPathExpression();
    }




}
