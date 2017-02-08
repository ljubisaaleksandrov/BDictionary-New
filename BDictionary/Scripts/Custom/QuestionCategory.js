var InventoryStaiton = InventoryStaiton || {};


InventoryStaiton.QuestionCategory = function () {
    var that = this;

    this.init = function () {
        $(document).ready(function () {
            if ($("#SelectedCategoryName").val() == "") {
                $("#AddAsParentQC").css({ "display": "none" });
            }
        });
    };

    /* Submit Category */

    this.SaveCategory = function(event)
    {
        event.preventDefault();
        var isModelValid = true;
        var isRootCategory = false;
        var nameElement = $("#QuestionCategoryViewModel_Name");
        var parentElement = $("#QuestionCategoryViewModel_Parent");
        var questionAnswerType = $("#QuestionCategoryViewModel_QuestionAnswerType");
        var modelErrorElement = $(".question-category-error");

        modelErrorElement.val('');

        if (parentElement.val() == "") {
            if (!confirm("The Parent Field is empty. Do you want to add a Root Category?")) {
                isModelValid = false;
                that.AppendError(modelErrorElement, 'Please select parent Category');
                $(parentElement).parent().parent().addClass('has-error');
            }
        }
        else {
            $(parentElement).parent().parent().removeClass('has-error')

            if ($(questionAnswerType).val() == 0) {
                isModelValid = false;
                that.AppendError(modelErrorElement, 'Please select Category Answer Type')
                $(questionAnswerType).parent().parent().addClass('has-error');
            }
            else {
                $(questionAnswerType).parent().parent().removeClass('has-error')
            }
        }

        if (nameElement.val() == "") {
            isModelValid = false;
            that.AppendError(modelErrorElement, 'Please enter Category Name');
            $(nameElement).parent().parent().addClass('has-error');
        }
        else {
            $(nameElement).parent().parent().removeClass('has-error')
        }

        if (isModelValid) {
            that.submitForm("AddOrUpdate");
        }
    }

    this.submitForm = function (formAction) {
        var $form = $(".existing-categories").parents('form');
        $form.attr('action', '/QuestionCategory/' + formAction);
        $form[0].submit();
    }

    this.AppendError = function (container, message) {
        var newLineSpan = "<br />";
        var starElement = "* ";
        var currentError = $(container).val();
        if (currentError != "")
            currentError = currentError + newLineSpan;
        var newContent = currentError + starElement + message;
        $(container).html(newContent);
        $(container).val(newContent);
    }

    /* Other Category Functionalities */
	
    this.AddParentCategory = function (event) {
        event.preventDefault();
        var selectedCategoryName = $("#SelectedCategoryName").val();
        $("#QuestionCategoryViewModel_Parent").val(selectedCategoryName);
        $("#QuestionCategoryParent").val(selectedCategoryName);
        $("#AddAsParentQC").css({ "display": "none" });
    }

    this.EditQuestionCategory = function (event) {
        event.preventDefault();
        var Id = $(".selected-category-options").attr('id');
        $("#SelectedCategoryId").val(Id);
        that.submitForm("EditCategory");
    }

    this.RemoveQuestionCategory = function (event) {
        event.preventDefault();
        var Id = $(".selected-category-options").attr('id');
        if (confirm("Are you sure, You want to delete selected Category?")) {
            $("#SelectedCategoryId").val(Id);
            that.submitForm("RemoveCategory");
        }
    }

    this.CancelEditing = function (event) {
        event.preventDefault();
        window.location = "/QuestionCategory/";
    }
	
    this.SelectAnswerType = function(type){
        // Number = 1; Text = 2
        if(type == "Number"){
            $("#QuestionCategoryViewModel_QuestionAnswerType").val(1);
            $("#NumberAnswerType").attr("src", "/Content/Images/number-icon-colour.bmp")
            $("#TextAnswerType").attr("src", "/Content/Images/letter-icon-white-black.bmp")
        }
        else if(type == "Text"){
            $("#QuestionCategoryViewModel_QuestionAnswerType").val(2);
            $("#NumberAnswerType").attr("src", "/Content/Images/number-icon-white-black.bmp")
            $("#TextAnswerType").attr("src", "/Content/Images/letter-icon-colour.bmp")
        }
    }

    this.SelectQuestionType = function () {
        if ($("#QuestionType").attr("src") == "/Content/Images/crazy-white-black.jpg") {
            $("#QuestionType").attr("src", "/Content/Images/crazy-colour.jpg");
            $("#QuestionHelperViewModel_IsShiz").val("True");
        }
        else {
            $("#QuestionType").attr("src", "/Content/Images/crazy-white-black.jpg");
            $("#QuestionHelperViewModel_IsShiz").val("False");
        }
    }

    /* Question Answers */

    this.RemoveQuestionAnswer = function (event, value) {
        event.preventDefault();
        $("#QuestionAnswerHelperViewModel_AnswerToRemove").val(value);
        that.submitForm('RemoveQuestionAnswer');
    }

    this.AddQuestionAnswer = function (event) {
        event.preventDefault();
        var value = $("#NewAnswer").val();

        if (value == ""){
            $("#NewAnswer").parent().addClass("has-error");
        }
        else {
            $("#NewAnswer").parent().removeClass("has-error");
            $("#QuestionAnswerHelperViewModel_AnswerToAdd").val(value);
            that.submitForm('AddQuestionAnswer');
        }
    }


    /* Question */

    this.RemoveQuestion = function (event, value) {
        event.preventDefault();
        $("#QuestionHelperViewModel_QuestionToRemove").val(value);
        that.submitForm('RemoveQuestion');
    }

    this.AddQuestion = function (event) {
        event.preventDefault();
        var isModelValid = true;
        var value = $("#NewQuestion").val();
        var answer = $("#SelectedAnswer option:selected").text();

        if (value == "") {
            $("#NewQuestion").parent().addClass("has-error");
            isModelValid = false;
        }
        else {
            $("#NewQuestion").parent().removeClass("has-error");
        }

        if (answer == "Select answer") {
            $("#SelectedAnswer").parent().addClass("has-error");
            isModelValid = false;
        }
        else {
            $("#SelectedAnswer").parent().removeClass("has-error");
        }

        if (isModelValid) {
            $("#QuestionHelperViewModel_SelectedAnswer").val(answer);
            $("#QuestionHelperViewModel_QuestionToAdd").val(value);
            that.submitForm('AddQuestion');
        }
	}



};