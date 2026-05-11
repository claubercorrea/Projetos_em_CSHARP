
 window.document.getElementById('ano-tual').textContent=new Date().getFullYear();

const togglePassword = document.querySelector('#togglePassword');
const password = document.querySelector('#inputPassword3');

togglePassword.addEventListener('click', function () {
    // Alterna o tipo de input entre 'password' e 'text'
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    
    // Alterna o ícone entre o olho aberto e o com barra
    this.classList.toggle('bi-eye');
    this.classList.toggle('bi-eye-slash');
});


// Captura o formulário (adicione um id="formLogin" na sua tag <form> no HTML)
const form = document.querySelector('form'); 

form.addEventListener('submit', function (event) {
    // Chama a função de validar e guarda o resultado (true ou false)
    const loginValido = validarLogin();

    if (!loginValido) {
        // Se a validação falhar, impede o formulário de ser enviado
        event.preventDefault(); 
    }
});

function validarLogin() {
    const email = document.querySelector('input[type="email"]').value;
    const senha = document.querySelector('#inputPassword3').value;

    if (email === "" || senha === "") {
        alert("Preencha todos os campos!");
        return false;
    }
    return true;
}


