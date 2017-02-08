var InventoryStaiton = InventoryStaiton || {};


InventoryStaiton.Question = function () {
    var that = this;

    this.init = function () {
        $(document).ready(function () {

        });
    }



    $('#applaySearch').click(function (event) {
        event.preventDefault();
        that.submitForm($(this).parents('form'), '/Question/Index');
    });


    this.submitForm = function ($form, formAction) {
        var inputSearchString = $("<input>").attr("type", "hidden").attr("name", "searchString").val($("#SearchString").val());
        var inputPageSize = $("<input>").attr("type", "hidden").attr("name", "pageSize").val($("#ItemsPerPageList").val());
        var sortOrder = $("<input>").attr("type", "hidden").attr("name", "sortOrder").val($("#sortOrder").val());

        $form.attr('action', formAction);
        $form.append($(inputPageSize))
             .append($(sortOrder))
             .append($(inputSearchString));
        $form[0].submit();
    }
}
