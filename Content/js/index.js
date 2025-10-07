window.addEventListener("scroll", function () {
        const header = document.getElementById("siteHeader");
        if (window.scrollY > 60) {
            header.classList.add("shrink");
        } else {
            header.classList.remove("shrink");
        }
    });