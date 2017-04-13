// Javascript commenting is two foward slashes
// Collision two files trying to modify same element, functions can provide their own scope so you don't havet to worry about collision (like namespacing)

// Semicolon is end of statement and can be the last statement, comma means there is a next statement (and you must have a next statement)

// Bower is like jquery - It is a javascript library to simplify html but it does client-side package managemetn

// Places all code we want into a function (namespace) and then calls the function - prevents collision of variables when everyghing is assigned to the global namespace

// Anonymous function is like a function without the name, you have to wrap then function in parenthesis and then add double () to the end to execute;
// You can assign parameters inside the function
(function () {

    //jQuery helps query the DOM for specific elements uses dollar signs ($) outside of () and # or . inside element name to replace document.getElementById
    //jQuery helps reduce inconsistencies between browser languages

    //Step 3: Sliding panel

    //Start jquery variables with dollar signs so that you know it is jquery
    //Toggle Class adds class if it doesn't exist and remove class if does exist, allows you to modify classes on events

    var $sidebarAndWrapper = $("#sidebar,#wrapper"); //Gets a wrapped set of DOM elements with comma
    var $icon = $("#SideBarToggle i.fa"); //Gets id called sidebar toggle with an i class called fa

    $("#SideBarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        } else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }
    });

    //Step 2: Replace with jquery (short version)

    // Replaces innerhtml in div with class id username
    //var ele = $("#username"); //must establish a variable
    //ele.text("Liam King Seacrest"); //takes action on variable

    // Javascript event
    // Browsers handle javascript differently so you have to find the best way to impliment


    //var main = $("#main");
    //main.on("mouseenter", function () {
    //    main.style = "background-color: #888;"; //Background color is case sensitivie
    //});

    //main.on("mouseleave", function () {
    //    main.style = "";
    //});

    //var menuItems = $("ul.menu li a"); //unorder list with class of menu, li for list, a for anchor
    //menuItems.on("click", function () {
    //    var me = $(this); //Use this to get out the specific list item being clicked (home, about, contact?)
    //    alert(me.text()); //If you don't include a parameter in text it returns the text
    //})

})();  //Use double parenthesis to execute

//Step 1: Long version

// Anonymous function would be the same as commented code here below
//function startup() {
//    var ele = document.getElementById("username"); 
//    ele.innerHTML = "Liam King Seacrest"; 

//    var main = document.getElementById("main");
//    main.onmouseenter = function () {
//        main.style.backgroundColor = "#888"; 
//    };

//    main.onmouseleave = function () {
//        main.style.backgroundColor = "";
//    }
//}

//startup();