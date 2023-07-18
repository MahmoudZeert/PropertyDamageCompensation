$(function () {
    // variable declarations
    var selectItemDataArray; // get data from cshtml view using :data- attributes for searching purposes
    var dateSaved;
    var editingState = "";
    //---------------------on click table cell event --------------------------------------------
    function onClickTable() {
        var currentRow = $(this).closest("tr");
        let damageItemRecordId = parseInt(currentRow.find("td").eq(0).text());
        $("#DamageItemRecordId").val(damageItemRecordId);
        let didi = parseInt(currentRow.find("td").eq(1).text());
        $("#DamageItemDefId").val(didi);
        let didp = parseInt(currentRow.find("td").eq(3).text());
        $("#DamageItemDefPrice").val(didp);
        let qty = parseInt(currentRow.find("td").eq(4).text());
        $("#Qty").val(qty);
        let ta = parseInt(currentRow.find("td").eq(5).text());
        $("#TotalAmount").val(ta);
    }
    //-----------------on click Add button event -------------------------
    function onClickAdd() {
        // initialize oldItemtotalAmount
        // display ongoin action
        $("#ongoingAction").text("Adding action in progress ");
        editingState = "ADDING";
        //make table and all input elements enabled
        $("#Date").prop("disabled", false);
        $("#DamageItemDefId").prop("disabled", false);
        $("#DamageItemDefId").css("pointer-events", "auto");
        $("#DamageItemDefPrice").prop("disabled", false);
        $("#Qty").prop("disabled", false);
        $("#IemTableId").css("pointer-events", "none");
        // hide Add button and show Save Cancel buttons
        $("#Addbtn").hide();
        $("#Savebtn").prop("hidden", false);
        $("#Cancelbtn").prop("hidden", false);
        //initialize input elements
        $("#DamageItemDefPrice").val("");
        $("#Qty").val("");
        $("#TotalAmount").val("");
        // save the Date value into dataSaved variable
        dateSaved = $("#Date").val();
    }
    // validate user input
    function DataAreValid() {
        let dateValue = $("#Date").val();
        if (dateValue === "") {
            $("#Date").focus();
            return {
                valid: false,
                message: "Please enter Date  !",
            };
        }
        let parseDate = $.datepicker.parseDate("dd/mm/yy", dateValue);
        if (parseDate === null) {
            return {
                valid: false,
                message: "Invalid Date  !",
            };
        }
        if (
            $("#DamageItemDefPrice").val().trim() === "0" ||
            parseInt($("#DamageItemDefPrice").val()) < 0 ||
            $("#DamageItemDefPrice").val().trim() === ""
        ) {
            $("#DamageItemDefPrice").focus();
            return {
                valid: false,
                message: "Invalid Item price !",
            };
        }
        if (
            $("#Qty").val().trim() === "0" ||
            parseInt($("#Qty").val()) < 0 ||
            $("#Qty").val().trim() === ""
        ) {
            $("#Qty").focus();
            return {
                valid: false,
                message: "Invalid Item Qty !",
            };
        }

        return {
            valid: true,
            message: "",
        };
    }
    function onClickSave() {
        //validate user inputs
        const response = DataAreValid();
        if (!response.valid) {
            ShowModal("Error", response.message);
            return;
        }
        // prepare the object for ajax call
        let url = "";
        let damageItemRecordId = 0;
        if (editingState === "EDITING") {
            url = "/Compensation/DamageSurvey/EditAssessedItem";
            damageItemRecordId = parseInt($("#DamageItemRecordId").val());
        }
        if (editingState === "ADDING") {
            url = "/Compensation/DamageSurvey/AddNewAssessedItem";
            damageItemRecordId = 0;
        }
        let damageSurveyId = $("#DamageSurveyId").val();
        let date = $("#Date").val();
        let applicationId = $("#ApplicationId").val();
        let personalInfoId = $("#PersonalInfoId").val();
        let engineerId = $("#EngineerId").val();
        // item definiation assessed item
        let damageItemDefId = $("#DamageItemDefId").val();
        let damageItemDefPrice = $("#DamageItemDefPrice").val();
        let qty = $("#Qty").val();
        let totalAmountForItem = $("#TotalAmount").val();
        const inputData = {
            DamageSurveyId: damageSurveyId,
            Date: date,
            ApplicationId: applicationId,
            PersonalInfoId: personalInfoId,
            EngineerId: engineerId,
            ItemAssessed: {
                Id: damageItemRecordId,
                DamageItemDefId: damageItemDefId,
                DamageItemDefPrice: damageItemDefPrice,
                Qty: qty,
                TotalAmount: totalAmountForItem,
            },
        };
        // call AddNewItem action method
        $.ajax({
            url: url,
            data: inputData,
            type: "POST",
            success: function (data) {
                ShowToast(
                    "Adding Item",
                    "This Item has been added successfuly"
                );
                $("#DamageItemAssessedList").html(data);
                $("#IemTableId td").on("click", onClickTable);
                $("#IemTableId").on("click", "#Deletebtn", onClickDelete);
                $("#IemTableId").on("click", "#Editbtn", onClickEdit);
                //make table and all input elements disable
                $("#Date").prop("disabled", true);
                $("#DamageItemDefId").prop("disabled", true);
                $("#DamageItemDefPrice").prop("disabled", true);
                $("#Qty").prop("disabled", true);
                $("#IemTableId").css("pointer-events", "auto");
                $("#DamageItemDefId").css("pointer-events", "auto");
                //-------------refresh Add Cancel and Save buttons accordingly--------
                $("#Addbtn").show();
                $("#Savebtn").prop("hidden", true);
                $("#Cancelbtn").prop("hidden", true);
                $("#ongoingAction").text("");
                editingState = "";
                refreshTableTotalAmountAndNumber();
            },
            error: function (xhr, ajaxOption, thrownError) {
                console.error(
                    thrownError +
                        "\r\n" +
                        xhr.statusText +
                        "\r\n" +
                        xhr.responseText
                );
            },
        });
    }

    function onClickCance() {
        $("#Date").val(dateSaved);
        refreshFirstRow();
        //make table and all input elements disable
        $("#Date").prop("disabled", true);
        $("#DamageItemDefId").prop("disabled", true);
        $("#DamageItemDefId").css("pointer-events", "none");
        $("#DamageItemDefPrice").prop("disabled", true);
        $("#Qty").prop("disabled", true);
        $("#IemTableId").css("pointer-events", "auto");
        // hide Add button and show Save Cancel buttons
        $("#Addbtn").show();
        $("#Savebtn").prop("hidden", true);
        $("#Cancelbtn").prop("hidden", true);
        $("#ongoingAction").text("");
        editingState = "";
    }
    function onClickEdit() {
        // display action name
        editingState = "EDITING";
        $("#ongoingAction").text("Editing action in progress");
        //make table and all input elements enabled
        $("#Date").prop("disabled", false);
        $("#DamageItemDefId").prop("disabled", false);
        $("#DamageItemDefId").css("pointer-events", "none");
        $("#DamageItemDefPrice").prop("disabled", false);
        $("#Qty").prop("disabled", false);
        $("#IemTableId").css("pointer-events", "none");
        // hide Add button and show Save Cancel buttons
        $("#Addbtn").hide();
        $("#Savebtn").prop("hidden", false);
        $("#Cancelbtn").prop("hidden", false);
        // save the Date value into dataSaved variable
        dateSaved = $("#Date").val();
    }
    function onClickDelete() {
        const self = this;
        const itemName = $(self).closest("tr").find("td:eq(2)").text();
        console.log($(self).closest("tr").find("td:eq(2)").text());
        DeleteConfirmationResponse(itemName).then(function (response) {
            if (response !== true) {
                return;
                // User clicked the Delete button
                // Perform the delete action here
            }
            let itemId = parseInt(
                $(self).closest("tr").find("td:eq(0)").text()
            );
            let damageSurveyId = parseInt($("#DamageSurveyId").val());
            // call DeleteDamageAssessedItem action method
            $.ajax({
                url: "/Compensation/DamageSurvey/DeleteDamageAssessedItem",
                data: { itemId: itemId, damageSurveyId: damageSurveyId },
                type: "POST",
                success: function (data) {
                    $("#DamageItemAssessedList").html(data);
                    $("#IemTableId td").on("click", onClickTable);
                    $("#IemTableId").on("click", "#Deletebtn", onClickDelete);
                    $("#IemTableId").on("click", "#Editbtn", onClickEdit);
                    //-------------refresh total count and amounts accordingly--------
                    refreshFirstRow();
                    refreshTableTotalAmountAndNumber();
                },
                error: function (xhr, ajaxOption, thrownError) {
                    console.error(
                        thrownError +
                            "\r\n" +
                            xhr.statusText +
                            "\r\n" +
                            xhr.responseText
                    );
                },
            });
        });
    }

    //------------------on change select element ---------------------------
    function onChangeSelect() {
        var id = parseInt($(this).val());
        var unitPrice = getItemPriceById(id, selectItemDataArray);
        $("#DamageItemDefPrice").val(unitPrice);
        $("#Qty").val(1).focus().select();
        $("#TotalAmount").val(unitPrice);
        $("#DamageSurveyTotalAmoutLabel").text();
        refreshTableTotalAmountAndNumber();
    }
    function onInputQty() {
        if (
            $("#Qty").val().trim() !== "" &&
            $("#DamageItemDefPrice").val().trim() !== ""
        ) {
            var total = $("#Qty").val() * $("#DamageItemDefPrice").val();
            $("#TotalAmount").val(total);
        }
    } // on input event
    function onInputUnitPrice() {
        if (
            $("#Qty").val().trim() !== "" &&
            $("#DamageItemDefPrice").val().trim() !== ""
        ) {
            var total = $("#Qty").val() * $("#DamageItemDefPrice").val();
            $("#TotalAmount").val(total);
        }
    }
    //------------------Define events --------------------------------------
    $("#IemTableId td").on("click", onClickTable);
    $("#Addbtn").on("click", onClickAdd);
    $("#Savebtn").on("click", onClickSave);
    $("#Cancelbtn").on("click", onClickCance);
    $("#IemTableId").on("click", "#Deletebtn", onClickDelete);
    $("#IemTableId").on("click", "#Editbtn", onClickEdit);
    $("#DamageItemDefId").on("change", onChangeSelect);
    $("#Qty").on("input", onInputQty);
    $("#DamageItemDefPrice").on("input", onInputUnitPrice);
    //----------------------------------------------------------------------
    function initializeInputElements() {
        // make select, and other input elements disable
        $("#Date").prop("disabled", true);
        $("#DamageItemDefId").prop("disabled", true);
        $("#DamageItemDefPrice").prop("disabled", true);
        $("#Qty").prop("disabled", true);
        $("#TotalAmount").prop("disabled", true);
    }
    function refreshTableTotalAmountAndNumber() {
        let count = parseInt($("#IemTableId tbody tr").length);
        if ($("#IemTableId tbody tr").length > 0) {
            let sum = 0;
            $("#IemTableId tbody tr").each(function () {
                sum += parseInt($(this).find("td:eq(5)").text());
            });
            $("#DamageSurveyTotalAmoutLabel").text(sum);
            $("#DamageSurveyTotalNumberLabel").text(count);
        } else {
            $("#DamageSurveyTotalAmoutLabel").text("");
            $("#DamageSurveyTotalNumberLabel").text("");
        }
    }
    function refreshFirstRow() {
        //if table is not empty , refresh the values of input elements with
        //those from the first row
        if ($("#IemTableId tbody tr").length > 0) {
            let firstRow = $("#IemTableId tbody").find("tr:first");

            let didi = parseInt(firstRow.find("td").eq(1).text());
            $("#DamageItemDefId").val(didi);
            let damageItemRecordId = parseInt(firstRow.find("td").eq(0).text());
            $("#DamageItemRecordId").val(damageItemRecordId);
            let didp = parseInt(firstRow.find("td").eq(3).text());
            $("#DamageItemDefPrice").val(didp);
            let qty = parseInt(firstRow.find("td").eq(4).text());
            $("#Qty").val(qty);
            let ta = parseInt(firstRow.find("td").eq(5).text());
            $("#TotalAmount").val(ta);
        } else {
            $("#DamageItemDefPrice").val("");
            $("#Qty").val("");
            $("#TotalAmount").val("");
        }
    }
    function fillselectItemDataArray() {
        selectItemDataArray = JSON.parse(
            JSON.stringify($("#ItemDefs").data("items"))
        );
    }
    function getItemPriceById(id, ItemDataArray) {
        var items = $.grep(ItemDataArray, function (e) {
            return id === e.id;
        });
        if (items.length === 0) {
            return 0;
        } else {
            return items[0].unitPrice;
        }
    }
    //show toast
    // Get the toast element
    function ShowToast(title, bodyText) {
        const toastElement = $("body #SuccessTost");

        // Create a new toast instance
        var toast = new bootstrap.Toast(toastElement);
        // Customize the toast
        toastElement.find(".toast-header strong").text(title);
        toastElement.find(".toast-body").text(bodyText);

        // Show the toast
        toast.show();
    }
    function ShowModal(title, bodyText) {
        const modal = $("body  #errorModal");

        modal.find(".modal-title").text(title);
        modal.find(".modal-body p").text(bodyText);
        modal.show();
        modal.find(".btn").on("click", function () {
            modal.hide();
        });
    }
    function DeleteConfirmationResponse(itemName) {
        const deleteModal = $("body #deleteModal");
        // Show the delete confirmation modal
        deleteModal.modal("show");
        $("body #deleteModal").find(".modal-body #itemName").text(itemName);
        // Create a deferred object to return a promise
        var deferred = $.Deferred();
        // Handle the delete action when the delete button in the modal is clicked
        $("body #deleteModal")
            .find(".modal-footer #deleteModalBtn")
            .on("click", function () {
                deleteModal.modal("hide");
                deferred.resolve(true);
            });
        $("body #deleteModal")
            .find(".modal-footer #cancelBtn")
            .on("click", function () {
                deleteModal.modal("hide");
                deferred.resolve(false);
            });
        // Return the promise that resolves to true or false
        return deferred.promise();
    }
    //------------------------------------------------------------------------
    initializeInputElements();
    refreshTableTotalAmountAndNumber();
    refreshFirstRow();
    fillselectItemDataArray();
});
