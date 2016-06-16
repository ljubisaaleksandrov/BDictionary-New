var InventoryStaiton = InventoryStaiton || {};


InventoryStaiton.QuestionCategory = function () {
    var that = this;

    this.init = function () {
        $(document).ready(function () {

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
        }

        if (nameElement.val() == "") {
            isModelValid = false;
            that.AppendError(modelErrorElement, 'Please enter Category Name');
            $(nameElement).parent().parent().addClass('has-error');
        }
        else {
            $(nameElement).parent().parent().removeClass('has-error')
        }

        if ($(questionAnswerType).val() == 0) {
            isModelValid = false;
            that.AppendError(modelErrorElement, 'Please select Category Answer Type')
            $(questionAnswerType).parent().parent().addClass('has-error');
        }
        else {
            $(questionAnswerType).parent().parent().removeClass('has-error')
        }

    }

    this.submitForm = function (formAction) {
        var $form = $(".existing-catogories").parents('form');
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
	
    this.EditQuestionCategory = function (event) {
        event.preventDefault();
        $.ajax({
            url: "/QuestionCategory/AddOrUpdate/"
        }).done(function (data) {
            $("#CategoryPartial").html(data);
        });
    }

    this.RemoveQuestionCategory = function (event) {
        event.preventDefault();
        if (!confirm("Are you sure, You want to delete selected Category?")) {
        }
    }

    this.CancelEditing = function (event) {
        event.preventDefault();
        $("#QuestionCategoryViewModel_Name").val('');
        $("#QuestionCategoryViewModel_Parent").val('');
        $(".question-category-error").html('');
        $(".question-category-error").val('');
        $("#QuestionCategoryViewModel_QuestionAnswerType").val(0);
        $("#NumberAnswerType").attr("src", "/Content/Images/number-icon-white-black.bmp")
        $("#TextAnswerType").attr("src", "/Content/Images/letter-icon-white-black.bmp")
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

    /* Question Answers */

	this.RemoveQuestionAnswer = function (event, value) {
	    event.preventDefault();
	    $("#QuestionAnswerHelper_AnswerToRemove").val(value);
	    that.submitForm('RemoveQuestionAnswer');
	}

	this.AddQuestionAnswer = function (event) {
	    event.preventDefault();
	    var value = $("#NewAnswer").val();
	    $("#QuestionAnswerHelper_AnswerToAdd").val(value);
	    that.submitForm('AddQuestionAnswer');
	}



};