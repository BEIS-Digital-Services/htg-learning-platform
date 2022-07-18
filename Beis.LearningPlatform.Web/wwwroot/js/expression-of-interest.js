
const eoi = {

    goToStep: function(stepNo) {
        ["1", "2", "3"].forEach((step) => {
            document.getElementById("step" + step).style.display = step == stepNo ? "block" : "none"
        })
        if (stepNo == 2) {
            document.getElementById("name").value = "";
            document.getElementById("email").value = "";
            document.getElementById("business-name").value = "";
            document.getElementById("phone-number").value = "";
            document.getElementById("erroremail").classList.remove("govuk-form-group--error");
            document.getElementById("errorname").classList.remove("govuk-form-group--error");
            document.getElementById("errorbusinessname").classList.remove("govuk-form-group--error");
            document.getElementById("errorphonenumber").classList.remove("govuk-form-group--error");
            document.getElementById("errorprivacyPolicy").classList.remove("govuk-form-group--error")
            document.getElementById("name").classList.remove("govuk-input--error");
            document.getElementById("email").classList.remove("govuk-input--error");
            document.getElementById("business-name").classList.remove("govuk-input--error");
            document.getElementById("phone-number").classList.remove("govuk-input--error");
            document.getElementById("errorsummary").style.display = "none";
            document.getElementById("privacyPolicy").checked = false;
        }
    },
    submit: function () {

        var isError = false;
        if (document.getElementById("name").value == "") {
            document.getElementById("errorname").classList.add("govuk-form-group--error");
            document.getElementById("name").classList.add("govuk-input--error");
            document.getElementById("name-error").style.display = "block";
            document.getElementById("errorsummary").style.display = "block";
            isError = true;
        } else {
            document.getElementById("name-error").style.display = "none";
            document.getElementById("errorname").classList.remove("govuk-form-group--error");
            document.getElementById("name").classList.remove("govuk-input--error");
        }

        if (document.getElementById("business-name").value == "") {
            document.getElementById("errorbusinessname").classList.add("govuk-form-group--error");
            document.getElementById("business-name").classList.add("govuk-input--error");
            document.getElementById("businessname-error").style.display = "block";
            document.getElementById("errorsummary").style.display = "block";
            isError = true;
        } else {
            document.getElementById("businessname-error").style.display = "none";
            document.getElementById("errorbusinessname").classList.remove("govuk-form-group--error");
            document.getElementById("business-name").classList.remove("govuk-input--error");
        }

        if (!this.validateEmail(document.getElementById("email").value) && document.getElementById("phone-number").value == "") {
            document.getElementById("erroremail").classList.add("govuk-form-group--error");
            document.getElementById("errorphonenumber").classList.add("govuk-form-group--error");
            document.getElementById("email").classList.add("govuk-input--error");
            document.getElementById("phone-number").classList.add("govuk-input--error");
            document.getElementById("contact-error").style.display = "block";
            document.getElementById("errorsummary").style.display = "block"
            isError = true;
        } else {
            document.getElementById("contact-error").style.display = "none";
            document.getElementById("erroremail").classList.remove("govuk-form-group--error");
            document.getElementById("email").classList.remove("govuk-input--error");
            document.getElementById("errorphonenumber").classList.remove("govuk-form-group--error");
            document.getElementById("phone-number").classList.remove("govuk-input--error");
        }



        if (!document.getElementById("privacyPolicy").checked) {
            document.getElementById("errorprivacyPolicy").classList.add("govuk-form-group--error")
            document.getElementById("privacyPolicy-error").style.display = "block";
            document.getElementById("errorsummary").style.display = "block"
            isError = true;
        } else {
            document.getElementById("errorprivacyPolicy").classList.remove("govuk-form-group--error")
            document.getElementById("privacyPolicy-error").style.display = "none";
        }


        if (!isError) {
            this.callAPI();
        }

    },
    validateEmail: function (email) {
        return email.match(
            /^(([^<>()[\]\\.,;:\s@@\"]+(\.[^<>()[\]\\.,;:\s@@\"]+)*)|(\".+\"))@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        );
    },
    removeError: function(processname) {

        if (processname == "contact") {
            if (document.getElementById("email").value != "" || document.getElementById("phone-number").value != "") {
                document.getElementById("contact-error").style.display = "none";
                document.getElementById("erroremail").classList.remove("govuk-form-group--error");
                document.getElementById("errorphonenumber").classList.remove("govuk-form-group--error");
                document.getElementById("email").classList.remove("govuk-input--error");
                document.getElementById("phone-number").classList.remove("govuk-input--error");
            }
        } else {
            document.getElementById("error" + processname).classList.remove("govuk-form-group--error");
            document.getElementById(processname + "-error").style.display = "none";
        }


        if (document.getElementById("name").value != "" && document.getElementById("business-name").value != "" && (document.getElementById("email").value != "" || document.getElementById("phone-number").value != "") && document.getElementById("privacyPolicy").checked) {
            document.getElementById("errorsummary").style.display = "none";
        }
    },
    showSubmitError: function () {
        //TODO: showSubmitError
    },
    showSubmitSuccess: function () {
        document.getElementById("step2").style.display = "none";
        document.getElementById("step3").style.display = "block";
    },

    callAPI: function () {
        const dto = {
            pageName: 'PageName',
            userName: document.getElementById("name").value,
            userEmail: document.getElementById("email").value,
            userBusinessName: document.getElementById("business-name").value,
            userPhone: document.getElementById("phone-number").value,
            optInMarketingEmail: true,
            optInMarketingPhone: true,
            optInReadPrivacy: true
        };

        const requestVerificationToken =
            document.querySelector("form[id='expresssion-of-interest-form'] input[name='__RequestVerificationToken']").value;
        
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
            .catch(error => console.error(error)); // TODO: Remove all console log messages - show error using validation UI.
        }
};
