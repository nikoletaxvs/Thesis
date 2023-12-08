var loader = document.getElementById("preloader");
window.addEventListener("load", function () {
    setTimeout(() => {
        loader.style.height = "100%";
        loader.style.width = "100%";
        loader.style.borderRadius = "50%";
        loader.style.visibility = "hidden";
    }, 200);
   
   
});