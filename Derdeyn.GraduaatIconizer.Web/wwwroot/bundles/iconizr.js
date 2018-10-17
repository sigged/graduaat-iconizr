(function (window)
{
    let loading = document.getElementById("loading");
    let generate = document.getElementById("generate");
    let result = document.getElementById("result");
    let type = document.getElementById("type");
    let module = document.getElementById("module");
    let group = document.getElementById("group");
    let iconizeform = document.getElementById("iconizeform");
    let placeholder = document.getElementById("placeholder");

    let buttonText = generate.innerText;
    //function loadXMLDoc() {
    //    var xmlhttp = new XMLHttpRequest();

    //    xmlhttp.onreadystatechange = function () {
    //        if (xmlhttp.readyState === XMLHttpRequest.DONE) {   // XMLHttpRequest.DONE == 4
    //            if (xmlhttp.status === 200) {
    //                document.getElementById("myDiv").innerHTML = xmlhttp.responseText;
    //            }
    //            else if (xmlhttp.status == 400) {
    //                alert('There was an error 400');
    //            }
    //            else {
    //                alert('something else other than 200 was returned');
    //            }
    //        }
    //    };

    //    xmlhttp.open("GET", "ajax_info.txt", true);
    //    xmlhttp.send();
    //}

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
        var modulevalue = module.value && module.value.length > 0 ? module.value : ' ';
        var groupvalue = group.value && group.value.length > 0 ? group.value : ' ';

        generate.innerText = "Even geduld...";
        generate.setAttribute('disabled', 'disabled');
        placeholder.style.display = 'none';
        result.style.display = 'none';
        loading.style.display = 'inline-block';
        result.src = "/generate/" +
            encodeURIComponent(modulevalue) + "/" +
            encodeURIComponent(groupvalue) + "/" +
            (type.value === "root" ? true : false)
    });


})(window);