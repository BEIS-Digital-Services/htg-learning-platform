
const eoi = {

    goToStep: function(stepNo) {
        ["1", "2", "3"].forEach((step) => {
            document.querySelector(".expression-of-interest #eoi-step" + step).style.display = step == stepNo ? "block" : "none"
        })
        if (stepNo == 2) {
            document.querySelector(".expression-of-interest #eoi-name").value = "";
            document.querySelector(".expression-of-interest #eoi-email").value = "";
            document.querySelector(".expression-of-interest #eoi-business-name").value = "";
            document.querySelector(".expression-of-interest #eoi-phone-number").value = "";
            document.querySelector(".expression-of-interest #eoi-erroremail").classList.remove("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-errorname").classList.remove("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-errorbusinessname").classList.remove("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-errorphonenumber").classList.remove("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-errorprivacyPolicy").classList.remove("govuk-form-group--error")
            document.querySelector(".expression-of-interest #eoi-name").classList.remove("govuk-input--error");
            document.querySelector(".expression-of-interest #eoi-email").classList.remove("govuk-input--error");
            document.querySelector(".expression-of-interest #eoi-business-name").classList.remove("govuk-input--error");
            document.querySelector(".expression-of-interest #eoi-phone-number").classList.remove("govuk-input--error");
            document.querySelector(".expression-of-interest #eoi-errorsummary").style.display = "none";
            document.querySelector(".expression-of-interest #eoi-privacyPolicy").checked = false;
            document.querySelector(".expression-of-interest #eoi-submission-error").style.display = "none";
        }
    },
    submit: function (pageName) {

        var isError = false;
        if (document.querySelector(".expression-of-interest #eoi-name").value == "") {
            document.querySelector(".expression-of-interest #eoi-errorname").classList.add("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-name").classList.add("govuk-input--error");
            document.querySelector(".expression-of-interest #eoi-name-error").style.display = "block";
            document.querySelector(".expression-of-interest #eoi-errorsummary").style.display = "block";
            isError = true;
        } else {
            document.querySelector(".expression-of-interest #eoi-name-error").style.display = "none";
            document.querySelector(".expression-of-interest #eoi-errorname").classList.remove("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-name").classList.remove("govuk-input--error");
        }

        if (document.querySelector(".expression-of-interest #eoi-business-name").value == "") {
            document.querySelector(".expression-of-interest #eoi-errorbusinessname").classList.add("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-business-name").classList.add("govuk-input--error");
            document.querySelector(".expression-of-interest #eoi-businessname-error").style.display = "block";
            document.querySelector(".expression-of-interest #eoi-errorsummary").style.display = "block";
            isError = true;
        } else {
            document.querySelector(".expression-of-interest #eoi-businessname-error").style.display = "none";
            document.querySelector(".expression-of-interest #eoi-errorbusinessname").classList.remove("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-business-name").classList.remove("govuk-input--error");
        }

        if (!this.validateEmail(document.querySelector(".expression-of-interest #eoi-email").value) && document.querySelector(".expression-of-interest #eoi-phone-number").value == "") {
            document.querySelector(".expression-of-interest #eoi-erroremail").classList.add("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-errorphonenumber").classList.add("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-email").classList.add("govuk-input--error");
            document.querySelector(".expression-of-interest #eoi-phone-number").classList.add("govuk-input--error");
            document.querySelector(".expression-of-interest #eoi-contact-error").style.display = "block";
            document.querySelector(".expression-of-interest #eoi-errorsummary").style.display = "block"
            isError = true;
        } else {
            document.querySelector(".expression-of-interest #eoi-contact-error").style.display = "none";
            document.querySelector(".expression-of-interest #eoi-erroremail").classList.remove("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-email").classList.remove("govuk-input--error");
            document.querySelector(".expression-of-interest #eoi-errorphonenumber").classList.remove("govuk-form-group--error");
            document.querySelector(".expression-of-interest #eoi-phone-number").classList.remove("govuk-input--error");
        }



        if (!document.querySelector(".expression-of-interest #eoi-privacyPolicy").checked) {
            document.querySelector(".expression-of-interest #eoi-errorprivacyPolicy").classList.add("govuk-form-group--error")
            document.querySelector(".expression-of-interest #eoi-privacyPolicy-error").style.display = "block";
            document.querySelector(".expression-of-interest #eoi-errorsummary").style.display = "block"
            isError = true;
        } else {
            document.querySelector(".expression-of-interest #eoi-errorprivacyPolicy").classList.remove("govuk-form-group--error")
            document.querySelector(".expression-of-interest #eoi-privacyPolicy-error").style.display = "none";
        }


        if (!isError) {
            this.callAPI(pageName);
        }

    },
    validateEmail: function (email) {
        return email.match(
            /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/
        );
    },
    removeError: function(processname) {

        if (processname == "contact") {
            if (document.querySelector(".expression-of-interest #eoi-email").value != "" || document.querySelector(".expression-of-interest #eoi-phone-number").value != "") {
                document.querySelector(".expression-of-interest #eoi-contact-error").style.display = "none";
                document.querySelector(".expression-of-interest #eoi-erroremail").classList.remove("govuk-form-group--error");
                document.querySelector(".expression-of-interest #eoi-errorphonenumber").classList.remove("govuk-form-group--error");
                document.querySelector(".expression-of-interest #eoi-email").classList.remove("govuk-input--error");
                document.querySelector(".expression-of-interest #eoi-phone-number").classList.remove("govuk-input--error");
            }
        } else {
            document.querySelector(".expression-of-interest #eoi-error" + processname).classList.remove("govuk-form-group--error");
            document.getElementById(processname + "-error").style.display = "none";
        }


        if (document.querySelector(".expression-of-interest #eoi-name").value != "" && document.querySelector(".expression-of-interest #eoi-business-name").value != "" && (document.querySelector(".expression-of-interest #eoi-email").value != "" || document.querySelector(".expression-of-interest #eoi-phone-number").value != "") && document.querySelector(".expression-of-interest #eoi-privacyPolicy").checked) {
            document.querySelector(".expression-of-interest #eoi-errorsummary").style.display = "none";
        }
    },
    showSubmitError: function () {
        document.querySelector(".expression-of-interest #eoi-submission-error").style.display = "block";
        document.querySelector(".expression-of-interest #eoi-errorsummary").style.display = "block"
        isError = true;
    },
    showSubmitSuccess: function () {
        document.querySelector(".expression-of-interest #eoi-step2").style.display = "none";
        document.querySelector(".expression-of-interest #eoi-step3").style.display = "block";
    },

    callAPI: function (pageName) {
        const dto = {
            pageName: pageName,
            userName: document.querySelector(".expression-of-interest #eoi-name").value,
            userEmail: document.querySelector(".expression-of-interest #eoi-email").value,
            userBusinessName: document.querySelector(".expression-of-interest #eoi-business-name").value,
            userPhone: document.querySelector(".expression-of-interest #eoi-phone-number").value,
            optInReadPrivacy: document.querySelector(".expression-of-interest #eoi-privacyPolicy").checked
        };

        const requestVerificationToken =
            document.querySelector("form[id='eoi-expresssion-of-interest-form'] input[name='__RequestVerificationToken']").value;
        
        fetch('/expression-of-interest', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'X-XSRF-TOKEN': requestVerificationToken
            },
            body: JSON.stringify(dto)
        })
            .then((response) => {
                if (response.status === 200)
                    this.showSubmitSuccess();
                else
                    this.showSubmitError();
            })
            .catch(error => console.error(error));
        }
};
