/// <reference path="../jquery-1.7.js" />
/// <reference path="../knockout-3.0.0.js" />   

ko.bindingHandlers.fadeVisible = {
    init: function (element, valueAccessor) {
        // Start visible/invisible according to initial value
        var shouldDisplay = valueAccessor();
        $(element).toggle(shouldDisplay);
    },
    update: function (element, valueAccessor) {
        // On update, fade in/out
        var shouldDisplay = valueAccessor();
        shouldDisplay ? $(element).fadeIn() : $(element).fadeOut();
    }
};

ko.bindingHandlers.jqButton = {
    init: function (element) {
        $(element).button(); // Turns the element into a jQuery UI button
    },
    update: function (element, valueAccessor) {
        var currentValue = valueAccessor();
        // Here we just update the "disabled" state, but you could update other properties too
        $(element).button("option", "disabled", currentValue.enable === false);
    }
};

ko.bindingHandlers.starRating = {
    init: function (element, valueAccessor) {
        $(element).addClass("starRating");
        for (var i = 0; i < 5; i++)
            $("<span>").appendTo(element);

        // Handle mouse events on the stars
        $("span", element).each(function (index) {
            $(this).hover(
                function () { $(this).prevAll().add(this).addClass("hoverChosen") },
                function () { $(this).prevAll().add(this).removeClass("hoverChosen") }
            ).click(function () {
                var observable = valueAccessor();  // Get the associated observable
                observable(index + 1);               // Write the new rating to it
            });
        });
    },
    update: function (element, valueAccessor) {
        // Give the first x stars the "chosen" class, where x <= rating
        var observable = valueAccessor();
        $("span", element).each(function (index) {
            $(this).toggleClass("chosen", index < observable());
        });
    }
};