(function (window)
{
    let loading = document.getElementById("loading");
    let generate = document.getElementById("generate");
    let result = document.getElementById("result");
    let type = document.getElementById("type");
    let top = document.getElementById("top");
    let bottom = document.getElementById("bottom");
    let iconizeform = document.getElementById("iconizeform");
    let placeholder = document.getElementById("placeholder");

    let buttonText = generate.innerText;

    iconizeform.addEventListener("submit", function (e) {
        e.preventDefault();
    });

    result.addEventListener('load', function (e) {
        setTimeout(function () {
            generate.innerText = buttonText;
            placeholder.style.display = 'none';
            loading.style.display = 'none';
            result.style.display = 'inline-block';
            generate.removeAttribute('disabled');
        }, 500);
    });

    result.addEventListener('error', function (e) {
        setTimeout(function () {
            generate.innerText = buttonText;
            placeholder.style.display = 'inline-block';
            result.style.display = 'none';
            loading.style.display = 'none';
            generate.removeAttribute('disabled');
        }, 500);
    });

    generate.addEventListener('click', function () {
        var toptext = top.value && top.value.length > 0 ? top.value : ' ';
        var bottomtext = bottom.value && bottom.value.length > 0 ? bottom.value : ' ';

        generate.innerText = "Even geduld...";
        generate.setAttribute('disabled', 'disabled');
        placeholder.style.display = 'none';
        result.style.display = 'none';
        loading.style.display = 'inline-block';
        result.src = "/generate/" +
            encodeURIComponent(toptext) + "/" +
            encodeURIComponent(bottomtext) + "/" +
            (type.value === "root" ? true : false);
    });


})(window);