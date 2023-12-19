var dvs = dvs || {};
(function () {
    dvs.utils = dvs.utils || {};

    dvs.utils.setCookieValue = function (key, value, expireDate, path) {
        var cookieValue = encodeURIComponent(key) + '=';

        if (value) {
            cookieValue = cookieValue + encodeURIComponent(value);
        }

        if (expireDate) {
            cookieValue = cookieValue + '; expires=' + expireDate.toUTCString();
        }

        if (path) {
            cookieValue = cookieValue + '; path=' + path;
        }

        document.cookie = cookieValue;
    };

    dvs.utils.getCookieValue = function (key) {
        var equalities = document.cookie.split('; ');
        for (var i = 0; i < equalities.length; i++) {
            if (!equalities[i]) {
                continue;
            }

            var splitted = equalities[i].split('=');
            if (splitted.length != 2) {
                continue;
            }

            if (decodeURIComponent(splitted[0]) === key) {
                return decodeURIComponent(splitted[1] || '');
            }
        }

        return null;
    };

    dvs.auth = dvs.auth || {};

    dvs.auth.tokenHeaderName = 'Authorization';
    dvs.auth.tokenCookieName = 'DVS.AuthToken';

    dvs.auth.getToken = function () {
        return dvs.utils.getCookieValue(dvs.auth.tokenCookieName);
    };

    dvs.auth.setToken = function (token, expireDate) {
        console.log('SetToken', token, expireDate);
        dvs.utils.setCookieValue(dvs.auth.tokenCookieName, token, expireDate, '/');
    };

    dvs.auth.clearToken = function () {
        dvs.auth.setToken();
    };

    dvs.auth.requestInterceptor = function (request) {
        var token = dvs.auth.getToken();
        request.headers.Authorization = token;

        return request;
    };

    dvs.swagger = dvs.swagger || {};

    dvs.swagger.openAuthDialog = function (loginCallback) {
        dvs.swagger.closeAuthDialog();

        var authDialog = document.createElement('div');
        authDialog.className = 'dialog-ux';
        authDialog.id = 'dvs-auth-dialog';

        authDialog.innerHTML = `<div class="backdrop-ux"></div>
        <div class="modal-ux">
            <div class="modal-dialog-ux">
                <div class="modal-ux-inner">
                    <div class="modal-ux-header">
                        <h3>登录</h3>
                        <button type="button" class="close-modal">
                            <svg width="20" height="20">
                                <use href="#close" xlink:href="#close"></use>
                            </svg>
                        </button>
                    </div>
                    <div class="modal-ux-content">
                        <div class="auth-form-wrapper"></div>
                        <div class="auth-btn-wrapper">
                            <button class="btn modal-btn auth btn-done button">取消</button>
                            <button type="submit" class="btn modal-btn auth authorize button">登录</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;

        var formWrapper = authDialog.querySelector('.auth-form-wrapper');

        // username
        createInput(formWrapper, 'username', '账号');

        // password
        createInput(formWrapper, 'password', '密码', 'password');

        // captcha
        createCaptcha(formWrapper, 'captcha', '验证码');

        document.getElementsByClassName('swagger-ui')[1].appendChild(authDialog);

        authDialog.querySelector('.btn-done.modal-btn').onclick = function () {
            dvs.swagger.closeAuthDialog();
        };

        authDialog.querySelector('.authorize.modal-btn').onclick = function () {
            dvs.swagger.login(loginCallback);
        };

        authDialog.querySelector('.close-modal').onclick = function () {
            dvs.swagger.closeAuthDialog();
        };

        // 加载验证码
        loadCaptcha();
    };

    dvs.swagger.closeAuthDialog = function () {
        if (document.getElementById('dvs-auth-dialog')) {
            document.getElementsByClassName('swagger-ui')[1].removeChild(document.getElementById('dvs-auth-dialog'));
        }
    };

    dvs.swagger.login = async function (callback) {
        var data = {
            password: document.getElementById('password').value,
            userName: document.getElementById('username').value,
            captcha: document.getElementById('captcha').value,
            captchaKey: document.getElementById('captcha-image').dataset.key,
            platform: 0,
        };

        await fetch(`${dvs.host}/api/basic/token/password`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
            .then((response) => response.json())
            .then(async (response) => {
                if (response.code === 200) {
                    var expireDate = new Date(response.data.expirationDate);
                    dvs.auth.setToken(response.data.token, expireDate);
                    callback();
                } else {
                    alert(response.message);
                    await loadCaptcha();
                }
            });
    };

    dvs.swagger.logout = function () {
        dvs.auth.clearToken();
    };

    function createCaptcha(container, id, title) {
        var wrapper = document.createElement('div');
        wrapper.className = 'form-item';

        var label = document.createElement('label');
        label.innerText = title;
        label.className = 'form-item-label';
        wrapper.appendChild(label);

        var section = document.createElement('section');
        section.className = 'form-item-control';
        wrapper.appendChild(section);

        var input = document.createElement('input');
        input.id = id;
        input.type = 'text';
        section.appendChild(input);

        var image = document.createElement('img');
        image.id = 'captcha-image';
        image.onclick = async () => {
            await loadCaptcha();
        };
        section.appendChild(image);

        container.appendChild(wrapper);
    }

    async function loadCaptcha() {
        fetch(`${dvs.host}/api/basic/Token/captcha`)
            .then((response) => response.json())
            .then((response) => {
                console.log('Captcha:', response);

                if (response.code == 200) {
                    var captcha = document.getElementById('captcha-image');
                    captcha.src = response.data.captcha;
                    captcha.dataset.key = response.data.key;
                }
            });
    }

    function createInput(container, id, title, type) {
        var wrapper = document.createElement('div');
        wrapper.className = 'form-item';

        var label = document.createElement('label');
        label.innerText = title;
        label.className = 'form-item-label';
        wrapper.appendChild(label);

        var section = document.createElement('section');
        section.className = 'form-item-control';
        wrapper.appendChild(section);

        var input = document.createElement('input');
        input.id = id;
        input.type = type ? type : 'text';
        input.style.width = '100%';
        section.appendChild(input);

        container.appendChild(wrapper);
    }
})();