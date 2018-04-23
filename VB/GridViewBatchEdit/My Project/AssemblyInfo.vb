' Developer Express Code Central Example:
' GridView - Batch Editing - A simple implementation of an EditItem template
' 
' This example demonstrates how to create a custom editor inside column's DataItem
' template when GridView is in Batch Edit mode.
' 
' 
' You can implement the
' EditItem template for a column by performing the following steps:
' 
' 1. Specify
' column's EditItem template:    settings.Columns.Add(column =>    {
' column.FieldName = "C1";      column.SetEditItemTemplateContent(c =>      {
' @Html.DevExpress().SpinEdit(spinSettings =>        {
' spinSettings.Name = "C1spinEdit";          spinSettings.Width =
' System.Web.UI.WebControls.Unit.Percentage(100);
' spinSettings.Properties.ClientSideEvents.KeyDown = "C1spinEdit_KeyDown";
' spinSettings.Properties.ClientSideEvents.LostFocus = "C1spinEdit_LostFocus";
' }).Render();      });    });
' 
' 
' 
' 2. Handle grid's client-side
' BatchEditStartEditing
' (https://help.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditStartEditingtopic)
' event to set the grid's cell values to the editor. It is possible to get the
' focused cell value using the e.rowValues
' (https://help.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridViewBatchEditStartEditingEventArgs_rowValuestopic)
' property:    function Grid_BatchEditStartEditing(s, e) {      var
' productNameColumn = s.GetColumnByField("C1");      if
' (!e.rowValues.hasOwnProperty(productNameColumn.index))        return;      var
' cellInfo = e.rowValues[productNameColumn.index];
' C1spinEdit.SetValue(cellInfo.value);      if (e.focusedColumn ===
' productNameColumn)        C1spinEdit.SetFocus();    }
' 
' 
' 
' 3. Handle the
' BatchEditEndEditing
' (https://help.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditEndEditingtopic)
' event to pass the value entered in the editor to the grid's cell:    function
' Grid_BatchEditEndEditing(s, e) {      var productNameColumn =
' s.GetColumnByField("C1");      if
' (!e.rowValues.hasOwnProperty(productNameColumn.index))        return;      var
' cellInfo = e.rowValues[productNameColumn.index];      cellInfo.value =
' C1spinEdit.GetValue();      cellInfo.text = C1spinEdit.GetText();
' C1spinEdit.SetValue(null);    } 4. The BatchEditRowValidating
' (https://help.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditRowValidatingtopic)
' event allows validating the grid's cell based on the entered value:    function
' Grid_BatchEditRowValidating(s, e) {      var productNameColumn =
' s.GetColumnByField("C1");      var cellValidationInfo =
' e.validationInfo[productNameColumn.index];      if (!cellValidationInfo) return;
' var value = cellValidationInfo.value;      if
' (!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) === "") {
' cellValidationInfo.isValid = false;        cellValidationInfo.errorText = "C1
' is required";      }    } 5. Finally, handle the editor's client-side KeyDown
' (https://documentation.devexpress.com/AspNet/DevExpressWebASPxEditorsScriptsASPxClientTextEdit_KeyDowntopic.aspx)
' and LostFocus
' (https://documentation.devexpress.com/AspNet/DevExpressWebASPxEditorsScriptsASPxClientEdit_LostFocustopic.aspx)
' events to emulate the behavior of standard grid editors when an end-user uses a
' keyboard or mouse:    var preventEndEditOnLostFocus = false;    function
' C1spinEdit_KeyDown(s, e) {      var keyCode =
' ASPxClientUtils.GetKeyCode(e.htmlEvent);      if (keyCode !== ASPxKey.Tab &&
' keyCode !== ASPxKey.Enter) return;      var moveActionName =
' e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";      if
' (grid.batchEditApi[moveActionName]()) {
' ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
' preventEndEditOnLostFocus = true;      }    }    function
' C1spinEdit_LostFocus(s, e) {      if (!preventEndEditOnLostFocus)
' grid.batchEditApi.EndEdit();      preventEndEditOnLostFocus = false;    }  See
' Also:
' http://www.devexpress.com/scid=T115096
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=T115130

Imports Microsoft.VisualBasic
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
<Assembly: AssemblyTitle("GridViewBatchEdit")>
<Assembly: AssemblyDescription("")>
<Assembly: AssemblyConfiguration("")>
<Assembly: AssemblyCompany("")>
<Assembly: AssemblyProduct("GridViewBatchEdit")>
<Assembly: AssemblyCopyright("Copyright ? 2013")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyCulture("")>

' Setting ComVisible to false makes the types in this assembly not visible 
' to COM components.  If you need to access a type in this assembly from 
' COM, set the ComVisible attribute to true on that type.
<Assembly: ComVisible(False)>

' The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("8606ff51-223c-4e33-b127-35d34d4fc5c1")>

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Revision and Build Numbers 
' by using the '*' as shown below:
<Assembly: AssemblyVersion("1.0.0.0")>
<Assembly: AssemblyFileVersion("1.0.0.0")>