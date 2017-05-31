﻿function setUpModalToggles(index: number, elem: Element) {
    const $toggle = $(elem);

    const targetModalSelector = $toggle.data("target");
    if (!targetModalSelector) {
        return;
    }

    $toggle.on("click",
        () => {
            $(targetModalSelector).modal("show");
        });
}

function hideErrorAndSuccessMessages() {
    $(".ui.error.message, .ui.success.message").addClass("hidden");
}

$(document).ready(() => {
    $("[data-toggle='modal']").each(setUpModalToggles);
    $(".ui.calendar").calendar();
});