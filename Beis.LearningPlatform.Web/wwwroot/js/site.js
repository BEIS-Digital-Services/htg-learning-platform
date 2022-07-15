var imported = document.createElement('script');
imported.src = "/js/webappsettings.js";
document.head.appendChild(imported);

var selectedCardIds = [];
document.addEventListener("DOMContentLoaded", function () {

    showAllElements(document.querySelectorAll(".cards"));

    checkAndAddHandler("#roundel1", "#card1");
    checkAndAddHandler("#roundel2", "#card2");
    checkAndAddHandler("#roundel3", "#card3");
    checkAndAddHandler("#roundel4", "#card4");

    function checkAndAddHandler(currentRoundelId, cardId) {
        if (document.querySelector(currentRoundelId)) {
            document.querySelector(currentRoundelId).addEventListener("click", () => {
                showElement(document.querySelector(cardId));
            });
        }
    }

    function showElement(el) {
        el.classList.remove("hidden-card");
        if (selectedCardIds && !selectedCardIds.includes(el.id)) {
            selectedCardIds.push(el.id);
            var item = {
                value: selectedCardIds,
                expiry: new Date().getTime() + (skillModuleExpiryInMinutes * 60 * 1000)
            }
            localStorage.setItem("selectedCardIds", JSON.stringify(item));
        }
    }

    function showAllElements(elements) {
        elements.forEach(el => { el.classList.remove("no-script-hidden-card"); });
    }

    var oldSelectedCardIds = JSON.parse(localStorage.getItem("selectedCardIds")) || [];

    var now = new Date();
    if (now.getTime() > oldSelectedCardIds.expiry) {
        localStorage.removeItem("selectedCardIds");
        return;
    }
    if (oldSelectedCardIds.value && oldSelectedCardIds.value.length > 0) {
        selectedCardIds = oldSelectedCardIds.value;
        oldSelectedCardIds.value.forEach(selectedCardId => {
            if (document.querySelector("#" + selectedCardId)) {
                document.querySelector("#" + selectedCardId).classList.remove("hidden-card");
            }
        });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    checkAndAddHandler("#header1", "1");
    checkAndAddHandler("#header2", "2");
    checkAndAddHandler("#header3", "3");

    function checkAndAddHandler(headerId, id) {
        if (document.querySelector(headerId)) {
            document.querySelector(headerId).addEventListener("click", () => {
                toggleThreeLevelsComponentElement(id);
            });
        }
    }

    function toggleThreeLevelsComponentElement(el) {
        if (document.querySelector("#level" + el)) {
            document.querySelector("#level" + el).classList.toggle("hidden-level");
        }
        if (document.querySelector("#copy" + el)) {
            document.querySelector("#copy" + el).classList.toggle("hidden-text");
        }
    }

    // Hide on first load if js:
    toggleThreeLevelsComponentElement(1);
    toggleThreeLevelsComponentElement(2);
    toggleThreeLevelsComponentElement(3);
});

var acc = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < acc.length; i++) {
    acc[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.display === "block") {
            panel.style.display = "none";
        } else {
            panel.style.display = "block";
        }
    });
}

//cms_component_NextPageRadioButton

document.addEventListener("DOMContentLoaded", function () {
    const cms_component_NextPageRadioButton_divjsenabled = document.getElementById("cms_component_NextPageRadioButton_divjsenabled");
    if (cms_component_NextPageRadioButton_divjsenabled) {

        document.getElementById("cms_component_NextPageRadioButton_divjsenabled").setAttribute("style", "");

        document.querySelectorAll("input[type=radio][name=cms_component_NextPageRadioButton_rdon]").forEach((input) => {
            input.addEventListener('change', function () {
                const rdoValue = this.value;

                if (rdoValue == "1") {
                    document.getElementById("cms_component_NextPageRadioButton_btn1").setAttribute("style", "");
                    document.getElementById("cms_component_NextPageRadioButton_btn2").setAttribute("style", "display:none;");
                    document.getElementById("cms_component_NextPageRadioButton_btn3").setAttribute("style", "display:none;");
                }
                else if (rdoValue == "2") {
                    document.getElementById("cms_component_NextPageRadioButton_btn1").setAttribute("style", "display:none;");
                    document.getElementById("cms_component_NextPageRadioButton_btn2").setAttribute("style", "");
                    document.getElementById("cms_component_NextPageRadioButton_btn3").setAttribute("style", "display:none;");
                }
                else {
                    document.getElementById("cms_component_NextPageRadioButton_btn1").setAttribute("style", "display:none;");
                    document.getElementById("cms_component_NextPageRadioButton_btn2").setAttribute("style", "display:none;");
                    document.getElementById("cms_component_NextPageRadioButton_btn3").setAttribute("style", "");
                }
            });
        });

    }

});



//dt, skills wizard additional info 
document.addEventListener("DOMContentLoaded", function () {

    const dt_checkbox_addinfo = document.getElementsByClassName("dt_checkbox_addinfo");
    if (dt_checkbox_addinfo && dt_checkbox_addinfo.length > 0) {
        const div_dt_ckh_additionalInfo = document.getElementById("div_dt_ckh_additionalInfo");

        if (dt_checkbox_addinfo[0].checked) {
            div_dt_ckh_additionalInfo.style.display = "block";
        }
        else {
            div_dt_ckh_additionalInfo.style.display = "none";
        }

        dt_checkbox_addinfo[0].addEventListener('change', (event) => {

            const dt_addinfo_txtarea = document.getElementsByClassName("dt_addinfo_txtarea")[0];

            if (event.currentTarget.checked) {
                div_dt_ckh_additionalInfo.style.display = "block";

            } else {
                div_dt_ckh_additionalInfo.style.display = "none";
                dt_addinfo_txtarea.value = "";
            }
        });

    }


});

function skillsThreeSubmit(formTypeNum, itemKey) {
    if (formTypeNum > 2) {
        localStorage.setItem(itemKey, 'true');
    }
}

function setImgItemLinkIcon(itemKey, spn_imgitemicon_std, spn_imgitemicon_completed) {

    var isCompleted = localStorage.getItem(itemKey, 'true');
    if (isCompleted == 'true') {
        var spnStd = document.getElementById(spn_imgitemicon_std);
        spnStd.style.display = "none";

        var spnCompleted = document.getElementById(spn_imgitemicon_completed);
        spnCompleted.style.display = "inline";
    }
}


