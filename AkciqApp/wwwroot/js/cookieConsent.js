const consent = document.getElementById('cookieConsents');

const cookieConsendFunc = () => {
    return `<div class="cookie-banner">
            <div>
                By using our website, you agree to our
                <a href="/Home/Privacy" class="cookie__policy">cookie policy</a>
            </div>
            <div class="btn btn-warning" onclick="acceptedCookie()">Accept</div>
        </div>`
}

const acceptedCookie = () => {
    const setCookie = (cname, cvalue, exdays) => {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }
    consent.innerHTML = "";
    setCookie("policy", "yes", 5);
}

const cookieAccept = () => {

    const getCookie = (cname) => {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
    const cookieResult = getCookie("policy");
    if (cookieResult === "yes") {
        consent.innerHTML = "";
    } else {
        consent.innerHTML = cookieConsendFunc();
    }
}

cookieAccept();