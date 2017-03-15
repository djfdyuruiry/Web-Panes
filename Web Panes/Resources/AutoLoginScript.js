(function () {
    let userName = "{{username}}";
    let password = "{{password}}";
    let domInsertEventType = "DOMNodeInserted";

    let attemptAutoLogin = function (username, password, isDynamicCallback) {
        let forms = document.getElementsByTagName("form");
        let webLoginForm = discoverLoginForm(forms);

        if (webLoginForm) {
            webLoginForm.userNameInput.value = userName;
            webLoginForm.passwordInput.value = password;

            if (isDynamicCallback) {
                document.removeEventListener(domInsertEventType, dynamicFormInsertHandler);
            }

            webLoginForm.form.submit();
        } else if (!isDynamicCallback) {
            debugger;
            // wait for login form to be dynamically inserted
            document.addEventListener(domInsertEventType, dynamicFormInsertHandler);
        }
    }

    let discoverLoginForm = function (forms) {
        let userNameInput;
        let passwordInput;

        for (let formIdx = 0; formIdx < forms.length; formIdx++) {
            let form = forms[formIdx];
            let inputs = form.getElementsByTagName("input");

            for (let inputIdx = 0; inputIdx < inputs.length; inputIdx++) {
                let input = inputs[inputIdx];
                let inputName = input.name;

                if (!inputName) {
                    continue;
                }

                switch (inputName) {
                    case "login":
                    case "username":
                        userNameInput = input;
                        break;
                    case "password":
                        passwordInput = input;
                        break;
                }
            }

            if (userNameInput && passwordInput) {
                return {
                    userNameInput: userNameInput,
                    passwordInput: passwordInput,
                    form: form
                };
            }
        };
    }

    let dynamicFormInsertHandler = function () {
        debugger;
        attemptAutoLogin(userName, password, true);
    };

    attemptAutoLogin(userName, password);
})();