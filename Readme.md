<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128549609/16.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T166450)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/GridViewBatchEdit/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/GridViewBatchEdit/Controllers/HomeController.vb))
* [Model.cs](./CS/GridViewBatchEdit/Models/Model.cs) (VB: [Model.vb](./VB/GridViewBatchEdit/Models/Model.vb))
* [AjaxLogin.js](./CS/GridViewBatchEdit/Scripts/AjaxLogin.js) (VB: [AjaxLogin.js](./VB/GridViewBatchEdit/Scripts/AjaxLogin.js))
* [_GridViewPartial.cshtml](./CS/GridViewBatchEdit/Views/Home/_GridViewPartial.cshtml)
* **[Index.cshtml](./CS/GridViewBatchEdit/Views/Home/Index.cshtml)**
<!-- default file list end -->
# GridView - Batch Editing - An EditItemTemplate with client-side unobtrusive validation 
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t166450/)**
<!-- run online end -->


This example is based  on <a href="https://www.devexpress.com/Support/Center/p/T115130">GridView - Batch Editing - A simple implementation of an EditItem template</a> and describes how to enable  client-side unobtrusive validation: <br><br>1) Wrap the GridView in a form:<br>


```cs
<form id="myForm">
    @Html.Action("GridViewPartial")
</form>
```


<br>2) Since GridView is bound to the list of values,  it's necessary to bind the EditItemTemplate editor to an item's model property from this list: <br>CS:<br>


```cs
 settings.Columns.Add(column =>
        {
            column.FieldName = "C1";
            column.SetEditItemTemplateContent(c =>
            {
                Html.DevExpress().SpinEditFor(m => m.FirstOrDefault().C1, spinSettings =>
                {
                    spinSettings.ShowModelErrors = true;
                    spinSettings.Properties.ValidationSettings.Display = Display.Dynamic;     
                    spinSettings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                    spinSettings.Properties.ClientSideEvents.KeyDown = "C1spinEdit_KeyDown";
                    spinSettings.Properties.ClientSideEvents.LostFocus = "C1spinEdit_LostFocus";
                }).Render();
            });
        });
```


<p>VB:</p>


```vb
   settings.Columns.Add(Sub(column)
                 column.FieldName = "C1"
                 column.SetEditItemTemplateContent(Sub(c)
                            Html.DevExpress().SpinEditFor(Function(m) m.FirstOrDefault().C1, Sub(spinSettings)
                                              spinSettings.Name = "C1"
                                              spinSettings.ShowModelErrors = True
                                              spinSettings.Properties.ValidationSettings.Display = Display.Dynamic
                                              spinSettings.Width = System.Web.UI.WebControls.Unit.Percentage(100)
                                              spinSettings.Properties.ClientSideEvents.KeyDown = "C1spinEdit_KeyDown"
                                              spinSettings.Properties.ClientSideEvents.LostFocus = "C1spinEdit_LostFocus"
                                                                                             End Sub).Render()
                                             End Sub)
    End Sub)
                                               
```


3)  The $.valid method in the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditEndEditingtopic">ASPxClientGridView.BatchEditEndEditing</a> event handler is required  to force validation when a currently edited cell loses focus:<br>


```js
 function Grid_BatchEditEndEditing(s, e) {
        var editItemTemplateColumn = s.GetColumnByField("C1");
        if (!e.rowValues.hasOwnProperty(editItemTemplateColumn.index))
            return;
        $("#myForm").valid();
        var cellInfo = e.rowValues[editItemTemplateColumn.index];
        cellInfo.value = C1.GetValue();
        cellInfo.text = C1.GetText();
        C1.SetValue(null);
 }
```


<br>4) The <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxEditorsScriptsASPxClientEdit_GetIsValidtopic">ASPxClientEdit.GetIsValid</a> and <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxEditorsScriptsASPxClientEdit_GetErrorTexttopic">ASPxClientEdit.GetErrorText</a> methods allow passing validation information from the editor to GridView cells in <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditRowValidatingtopic">ASPxClientGridView.BatchEditRowValidating</a>:<br>


```js
  function Grid_BatchEditRowValidating(s, e) {
        var editItemTemplateColumn = s.GetColumnByField("C1");
        var cellValidationInfo = e.validationInfo[editItemTemplateColumn.index];
        if (!cellValidationInfo)
            return;
        cellValidationInfo.isValid = C1.GetIsValid();
        cellValidationInfo.errorText = C1.GetErrorText();
   }
```


<br>
<p><strong>See Also:<br></strong><a href="https://www.devexpress.com/Support/Center/p/T115096">ASPxGridView - Batch Editing - A simple implementation of an EditItem template</a> <br><a href="https://www.devexpress.com/Support/Center/p/T115130">GridView - Batch Editing - A simple implementation of an EditItem template</a><br><br></p>

<br/>


