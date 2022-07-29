
const eoi = {

    goToStep: function(stepNo) {
        ["1", "2", "3"].forEach((step) => {
            document.querySelector("#eoi-step" + step).style.display = step == stepNo ? "block" : "none"
        });
        document.querySelector("#eoi-form-top-line").style.display = stepNo > 1 ? "block" : "none";
        if (stepNo == 2) {
            document.querySelector("#eoi-name").value = "";
            document.querySelector("#eoi-email").value = "";
            document.querySelector("#eoi-business-name").value = "";
            document.querySelector("#eoi-phone-number").value = "";
            document.querySelector("#eoi-erroremail").classList.remove("govuk-form-group--error");
            document.querySelector("#eoi-errorname").classList.remove("govuk-form-group--error");
            document.querySelector("#eoi-errorbusinessname").classList.remove("govuk-form-group--error");
            document.querySelector("#eoi-errorphonenumber").classList.remove("govuk-form-group--error");
            document.querySelector("#eoi-errorprivacyPolicy").classList.remove("govuk-form-group--error")
            document.querySelector("#eoi-name").classList.remove("govuk-input--error");
            document.querySelector("#eoi-email").classList.remove("govuk-input--error");
            document.querySelector("#eoi-business-name").classList.remove("govuk-input--error");
            document.querySelector("#eoi-phone-number").classList.remove("govuk-input--error");
            document.querySelector("#eoi-errorsummary").style.display = "none";
            document.querySelector("#eoi-privacyPolicy").checked = false;
            document.querySelector("#eoi-submission-error").style.display = "none";
            document.querySelector("#eoi-email-error").style.display = "none";
        }
    },
    submit: function (pageName) {

        let isError = false;
        if (document.querySelector("#eoi-name").value == "") {
            document.querySelector("#eoi-errorname").classList.add("govuk-form-group--error");
            document.querySelector("#eoi-name").classList.add("govuk-input--error");
            document.querySelector("#eoi-name-error").style.display = "block";
            document.querySelector("#eoi-errorsummary").style.display = "block";
            isError = true;
        } else {
            document.querySelector("#eoi-name-error").style.display = "none";
            document.querySelector("#eoi-errorname").classList.remove("govuk-form-group--error");
            document.querySelector("#eoi-name").classList.remove("govuk-input--error");
        }

        if (document.querySelector("#eoi-business-name").value == "") {
            document.querySelector("#eoi-errorbusinessname").classList.add("govuk-form-group--error");
            document.querySelector("#eoi-business-name").classList.add("govuk-input--error");
            document.querySelector("#eoi-businessname-error").style.display = "block";
            document.querySelector("#eoi-errorsummary").style.display = "block";
            isError = true;
        } else {
            document.querySelector("#eoi-businessname-error").style.display = "none";
            document.querySelector("#eoi-errorbusinessname").classList.remove("govuk-form-group--error");
            document.querySelector("#eoi-business-name").classList.remove("govuk-input--error");
        }
        let emailInput = document.querySelector("#eoi-email");
        if ((emailInput.value == "") && document.querySelector("#eoi-phone-number").value == "") {
            document.querySelector("#eoi-erroremail").classList.add("govuk-form-group--error");
            document.querySelector("#eoi-errorphonenumber").classList.add("govuk-form-group--error");
            document.querySelector("#eoi-email").classList.add("govuk-input--error");
            document.querySelector("#eoi-phone-number").classList.add("govuk-input--error");
            document.querySelector("#eoi-contact-error").style.display = "block";
            document.querySelector("#eoi-errorsummary").style.display = "block";
            document.querySelector("#eoi-email-error").style.display = "none";
            isError = true;
        }
        else if (emailInput.value != "" && !this.validateEmail(emailInput.value)) {
            document.querySelector("#eoi-erroremail").classList.add("govuk-form-group--error");
            document.querySelector("#eoi-errorsummary").style.display = "block";
            document.querySelector("#eoi-email-error").classList.add("govuk-input--error");
            document.querySelector("#eoi-email-error").style.display = "block";
            document.querySelector("#eoi-contact-error").style.display = "none";
            isError = true;
        } else {
            document.querySelector("#eoi-contact-error").style.display = "none";
            document.querySelector("#eoi-erroremail").classList.remove("govuk-form-group--error");
            document.querySelector("#eoi-email").classList.remove("govuk-input--error");
            document.querySelector("#eoi-errorphonenumber").classList.remove("govuk-form-group--error");
            document.querySelector("#eoi-phone-number").classList.remove("govuk-input--error");
            document.querySelector("#eoi-email-error").classList.remove("govuk-input--error");
            document.querySelector("#eoi-email-error").style.display = "none";
        }



        if (!document.querySelector("#eoi-privacyPolicy").checked) {
            document.querySelector("#eoi-errorprivacyPolicy").classList.add("govuk-form-group--error")
            document.querySelector("#eoi-privacyPolicy-error").style.display = "block";
            document.querySelector("#eoi-errorsummary").style.display = "block"
            isError = true;
        } else {
            document.querySelector("#eoi-errorprivacyPolicy").classList.remove("govuk-form-group--error")
            document.querySelector("#eoi-privacyPolicy-error").style.display = "none";
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
            if (document.querySelector("#eoi-email").value != "" || document.querySelector("#eoi-phone-number").value != "") {
                document.querySelector("#eoi-contact-error").style.display = "none";
                document.querySelector("#eoi-erroremail").classList.remove("govuk-form-group--error");
                document.querySelector("#eoi-errorphonenumber").classList.remove("govuk-form-group--error");
                document.querySelector("#eoi-email").classList.remove("govuk-input--error");
                document.querySelector("#eoi-phone-number").classList.remove("govuk-input--error");
            }
        } else {
            document.querySelector("#eoi-error" + processname).classList.remove("govuk-form-group--error");
            document.getElementById(processname + "-error").style.display = "none";
        }


        if (document.querySelector("#eoi-name").value != "" && document.querySelector("#eoi-business-name").value != "" && (document.querySelector("#eoi-email").value != "" || document.querySelector("#eoi-phone-number").value != "") && document.querySelector("#eoi-privacyPolicy").checked) {
            document.querySelector("#eoi-errorsummary").style.display = "none";
        }
    },
    showSubmitError: function () {
        document.querySelector("#eoi-submission-error").style.display = "block";
        document.querySelector("#eoi-errorsummary").style.display = "block"
    },
    showSubmitSuccess: function () {
        document.querySelector("#eoi-step2").style.display = "none";
        document.querySelector("#eoi-step3").style.display = "block";
    },

    callAPI: function (pageName) {
        const dto = {
            pageName: pageName,
            userName: document.querySelector("#eoi-name").value,
            userEmail: document.querySelector("#eoi-email").value,
            userBusinessName: document.querySelector("#eoi-business-name").value,
            userPhone: document.querySelector("#eoi-phone-number").value,
            optInReadPrivacy: document.querySelector("#eoi-privacyPolicy").checked
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
