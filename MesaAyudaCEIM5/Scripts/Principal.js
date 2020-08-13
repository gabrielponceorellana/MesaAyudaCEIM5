
var tabsFn = (function () {

    function init() {
        setHeight();
    }

    function setHeight() {
        var $tabPane = $('.tab-pane'),
            tabsHeight = $('.nav-tabs').height();

        $tabPane.css({
            height: tabsHeight + 200
        });
    }

    $(init);
})();