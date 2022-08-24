var selectedIds = [];
var compareButtonsEnabled = false;

document.addEventListener("DOMContentLoaded", function () {
    var checkBoxes = null;


    document.querySelectorAll("form").forEach(f => f.addEventListener("submit", validateCompareButton));

    checkBoxes = document.querySelectorAll(".button__input");
    checkBoxes.forEach(c => {
        c.addEventListener("click", manageProducts);
        c.checked = false;
        c.removeAttribute("disabled");
        c.disabled = false;
    });

    ctProductFilter.init();

    var previouslySelectedIds = document.querySelector("#selectedProductIds");
    if (previouslySelectedIds && previouslySelectedIds.value !== "") {
        var productIds = previouslySelectedIds.value.split(",");
        productIds.forEach(p => document.querySelector("#prod-" + p).click());
    }
});

function manageProducts(e) {
    if (this.className.indexOf("active") < 0) {
        if (selectedIds.length === 3) {
            this.disabled = "disabled";
            e.preventDefault();
            return;
        }

        if (!selectedIds.includes(e.id)) {
            selectedIds.push(this.id);
            this.className = "govuk-button button__input compare active";
            this.textContent = "- Compare";
        }
    }
    else {
        selectedIds.splice(selectedIds.indexOf(this.id), 1);
        this.className = "govuk-button button__input compare";
        this.textContent = "+ Compare";
    }

    this.closest(".formDiv").querySelector('[name="selectedProductIds"]').value = selectedIds.join(",").replaceAll("prod-", "");
    this.closest(".formDiv").querySelectorAll(".button__input").forEach(c => {
        if (selectedIds.length === 3 && !selectedIds.includes(c.id)) {
            c.classList.add("disabled");
        } else {
            if (c.className.indexOf("active") < 0) {
                c.classList.remove("disabled");
            }
        }
    });

    toggleGroupCompareButtons(this);
    toggleFormDivElements(this);
}

function toggleGroupCompareButtons(currentButton) {
    var compareButtons = currentButton.closest(".formDiv").querySelectorAll(".compareButton");

    if (selectedIds.length > 1) {
        compareButtons.forEach(b => {
            if (!compareButtonsEnabled) {
                b.removeAttribute("disabled");
                b.className = "compareButton govuk-button compare-btn primary";
            }
            b.value = "Compare " + selectedIds.length + " products";
        });
        compareButtonsEnabled = true;
    } else if (selectedIds.length <= 1) {
        compareButtons.forEach(b => {
            if (compareButtonsEnabled) {
                b.className = b.className + " govuk-button--disabled";
                b.disabled = "disabled";
            }
            b.value = "Compare products";
        });
        compareButtonsEnabled = false;
    }
}

function toggleFormDivElements(currentButton) {
    var formDivs = document.querySelectorAll(".formDiv");
    var currentFormDiv = currentButton.closest(".formDiv");
    if (currentButton.className.indexOf("active") > 0) {
        formDivs.forEach(fd => {
            if (fd.id !== currentFormDiv.id) {
                fd.style.display = "none";
            } else {
                fd.style.display = "block";
            }
        });
    } else {
        var anyBoxChecked = false;
        currentFormDiv.querySelectorAll(".button__input").forEach(c => {
            if (c.className.indexOf("active") > 0) {
                anyBoxChecked = true;
            }
        });

        if (!anyBoxChecked) {
            formDivs.forEach(fd => {
                fd.style.display = "block";
            });
        }
    }
}

function validateCompareButton(e) {
    var checkedCount = 0;
    var currentButtons = this.parentElement.querySelectorAll(".button__input");
    currentButtons.forEach(c => {
        if (c.className.indexOf("active") > 0) {
            checkedCount++;
        }
    });

    if (checkedCount === 0) {
        this.parentElement.querySelectorAll(".active").forEach(b => {
            b.className = b.className + " govuk-button--disabled";
            b.disabled = "disabled";
        });
        e.preventDefault();
        return false;
    }
    return true;
}

const ctProductFilter = {
    productCategoryFilters: [],
    expectedProductCategories: [],
    init: function () {
        this.productCategoryFilters = Array.from(document.querySelectorAll(".productCategory"));
        this.expectedProductCategories = this.productCategoryFilters.map(x => x.getAttribute("data-id"));

        this.setVisibleCategories();
        this.checkVisibleCategories();
        this.productCategoryFilters.forEach(pcf => pcf.addEventListener("change", ctProductFilter.toggleProductsBasedOnCategory));
    },

    GetCheckedCategoryIds: function () {
        const hashValue = location.hash ? location.hash.replace("#", "") : null;
        if (!hashValue) {
            return [];
        }
        return hashValue.split(",").filter(x => this.expectedProductCategories.includes(x));
    },

    setCheckedCategoryIds: function (checkedCategoryIds) {
        history.replaceState(null, "", checkedCategoryIds && checkedCategoryIds.length ? "#" + checkedCategoryIds.toString() : "#");
    },

    checkVisibleCategories: function () {
        const checkedCategoryIds = this.GetCheckedCategoryIds();

        this.productCategoryFilters.forEach(elmnt => {
            elmnt.checked = checkedCategoryIds.includes(elmnt.getAttribute("data-id"));
        });
    },

    setVisibleCategories: function () {
        const checkedCategoryIds = this.GetCheckedCategoryIds();
        const formDivs = document.querySelectorAll(".formDiv");

        formDivs.forEach(d => {
            d.style.display = "none";
        });

        if (checkedCategoryIds.length === 0) {
            formDivs.forEach(d => {
                d.style.display = "block";
            });
        } else {
            checkedCategoryIds.forEach(r => {
                document.querySelector("#formDiv-" + r).style.display = "block";
            });
        }
    },

    toggleProductsBasedOnCategory: function () {
        document.querySelector("#allProductsDiv").style.display = "none";

        ctProductFilter.toggleIndividualAndGroupCompareButtons();

        const checkedCategoryIds = ctProductFilter.GetCheckedCategoryIds();
        ctProductFilter.productCategoryFilters.forEach(r => {
            const dataId = r.getAttribute("data-id");
            if (r.checked && !checkedCategoryIds.includes(dataId)) {
                checkedCategoryIds.push(dataId);
            } else if (!r.checked && checkedCategoryIds.includes(dataId)) {
                checkedCategoryIds.splice(checkedCategoryIds.indexOf(dataId), 1);
            }
        });
        ctProductFilter.setCheckedCategoryIds(checkedCategoryIds);

        ctProductFilter.setVisibleCategories();
    },

    toggleIndividualAndGroupCompareButtons: function () {
        document.querySelectorAll(".formDiv").forEach(f => {
            selectedIds = [];
            f.querySelectorAll(".button__input").forEach(c => {
                c.className = "govuk-button button__input compare";
                c.removeAttribute("disabled");
            });
            f.querySelectorAll(".compareButton").forEach(b => {
                b.value = "Compare products";
                b.disabled = "disabled";
                b.className = b.className + " govuk-button--disabled";
            });
            compareButtonsEnabled = false;
        });
    }
}