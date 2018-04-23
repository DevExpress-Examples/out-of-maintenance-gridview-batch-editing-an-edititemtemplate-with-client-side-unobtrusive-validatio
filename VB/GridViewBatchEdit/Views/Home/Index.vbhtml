<script type="text/javascript">
    function Grid_BatchEditStartEditing(s, e) {
        var editItemTemplateColumn = s.GetColumnByField("C1");
        if (!e.rowValues.hasOwnProperty(editItemTemplateColumn.index))
            return;
        var cellInfo = e.rowValues[editItemTemplateColumn.index];
        C1.SetIsValid(true);
        C1.SetValue(cellInfo.value);      
        if (e.focusedColumn === editItemTemplateColumn)
            C1.SetFocus();
    }
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
    function Grid_BatchEditRowValidating(s, e) {
        var editItemTemplateColumn = s.GetColumnByField("C1");
        var cellValidationInfo = e.validationInfo[editItemTemplateColumn.index];
        if (!cellValidationInfo)
            return;
        cellValidationInfo.isValid = C1.GetIsValid();
        cellValidationInfo.errorText = C1.GetErrorText();
    }
    var preventEndEditOnLostFocus = false;
    function C1spinEdit_KeyDown(s, e) {
        var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
        if (keyCode === ASPx.Key.Esc) {
            var cellInfo = grid.batchEditApi.GetEditCellInfo();
            window.setTimeout(function () {
                grid.SetFocusedCell(cellInfo.rowVisibleIndex, cellInfo.column.index);
            }, 0);
            s.GetInputElement().blur();
            return;
        }
        if (keyCode !== ASPx.Key.Tab && keyCode !== ASPx.Key.Enter) return;
        var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
        if (grid.batchEditApi[moveActionName]()) {
            ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
            preventEndEditOnLostFocus = true;
        }
    }
    function C1spinEdit_LostFocus(s, e) {
        if (!preventEndEditOnLostFocus)
            grid.batchEditApi.EndEdit();
        preventEndEditOnLostFocus = false;
    }
</script>
<form id="myForm">
    @Html.Action("GridViewPartial")
</form>
