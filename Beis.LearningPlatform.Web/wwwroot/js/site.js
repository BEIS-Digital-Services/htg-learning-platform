var imported = document.createElement('script');
imported.src = "/js/webappsettings.js";
document.head.appendChild(imported);

var selectedCardIds = [];
document.addEventListener("DOMContentLoaded", function () {

    showJsGoBackLinks();
    toggleJsDisplayElements();

    function showJsGoBackLinks() {
        const jsLinkContainers = document.querySelector(".js-go-back-link-container");
        if (jsLinkContainers) {
            jsLinkContainers.style.display = "block";

            const jsLinks = document.querySelector(".js-go-back-link-container a.govuk-back-link");
            if (jsLinks) {
                jsLinks.addEventListener("click", () => {
                    history.go(-1);
                });
            }
        }
    }

    // Allows showing elements inline/block etc - using attribute js-style-display.
    function toggleJsDisplayElements() {
        const jsStyleElmnts = document.querySelectorAll("[data-js-display]");
        if (jsStyleElmnts) {
            jsStyleElmnts.forEach(elmnt => {
                elmnt.style.display = elmnt.getAttribute("data-js-display");
            });            
        }
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
        let expanded = this.getAttribute('aria-expanded') === 'true';
        this.setAttribute('aria-expanded', !expanded);
        
        var panel = this.nextElementSibling;
        panel.hidden = expanded;
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

    var elmnts = document.querySelectorAll('[data-tabclick]'); 
    for (var i = 0; i < elmnts.length; i++) {
        if (elmnts[i].getAttribute("data-tabclick") === "true" && !elmnts[i].onkeydown) {

            elmnts[i].onkeydown = function (event) {
                switch (event.which) {
                    case 32: { // KEY_SPACE
                        event.stopPropagation;
                        this.click();
                        return false;
                    }
                }
                return true;
            };
        }
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

function keyHandler(event, func) {
    switch (event.which) {
        case KEY_SPACE: {
            event.stopPropagation;
            return func();
            break;
        }
    }
    return true;
}
