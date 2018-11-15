<!-- default file list -->
*Files to look at*:

* [FilterConfig.cs](./CS/GridViewBatchEdit/App_Start/FilterConfig.cs) (VB: [FilterConfig.vb](./VB/GridViewBatchEdit/App_Start/FilterConfig.vb))
* [RouteConfig.cs](./CS/GridViewBatchEdit/App_Start/RouteConfig.cs) (VB: [RouteConfig.vb](./VB/GridViewBatchEdit/App_Start/RouteConfig.vb))
* [WebApiConfig.cs](./CS/GridViewBatchEdit/App_Start/WebApiConfig.cs) (VB: [WebApiConfig.vb](./VB/GridViewBatchEdit/App_Start/WebApiConfig.vb))
* [HomeController.cs](./CS/GridViewBatchEdit/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/GridViewBatchEdit/Controllers/HomeController.vb))
* [Model.cs](./CS/GridViewBatchEdit/Models/Model.cs) (VB: [Model.vb](./VB/GridViewBatchEdit/Models/Model.vb))
* [_GridViewPartial.cshtml](./CS/GridViewBatchEdit/Views/Home/_GridViewPartial.cshtml)
* **[Index.cshtml](./CS/GridViewBatchEdit/Views/Home/Index.cshtml)**
<!-- default file list end -->
# GridView - Batch Editing - An EditItemTemplate with client-side unobtrusive validation 


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


