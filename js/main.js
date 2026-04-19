// ===== NAVBAR HAMBURGER =====
const hamburger = document.querySelector('.hamburger');
const navLinks = document.querySelector('.nav-links');

if (hamburger) {
  hamburger.addEventListener('click', () => {
    navLinks.classList.toggle('open');
  });
}

// Cerrar menú al hacer click en un link
document.querySelectorAll('.nav-links a').forEach(link => {
  link.addEventListener('click', () => {
    if (navLinks) navLinks.classList.remove('open');
  });
});

// Marcar link activo
const currentPage = window.location.pathname.split('/').pop() || 'index.html';
document.querySelectorAll('.nav-links a').forEach(link => {
  const href = link.getAttribute('href');
  if (href === currentPage || (currentPage === '' && href === 'index.html')) {
    link.classList.add('active');
  }
});

// ===== TOAST NOTIFICATION =====
function showToast(message) {
  let toast = document.getElementById('toast');
  if (!toast) {
    toast = document.createElement('div');
    toast.id = 'toast';
    toast.className = 'toast';
    document.body.appendChild(toast);
  }
  toast.textContent = message;
  toast.classList.add('show');
  setTimeout(() => toast.classList.remove('show'), 3500);
}

// ===== VALIDACION FORMULARIO CITAS =====
const citaForm = document.getElementById('formCita');
if (citaForm) {
  citaForm.addEventListener('submit', function (e) {
    e.preventDefault();
    let valid = true;

    const campos = [
      { id: 'nombre', msg: 'Ingresa tu nombre completo.' },
      { id: 'cedula', msg: 'Ingresa tu número de cédula.' },
      { id: 'telefono', msg: 'Ingresa tu teléfono.' },
      { id: 'email', msg: 'Ingresa tu correo electrónico.' },
      { id: 'especialidad', msg: 'Selecciona una especialidad.' },
      { id: 'medico', msg: 'Selecciona un médico.' },
      { id: 'fecha', msg: 'Selecciona la fecha de la cita.' },
      { id: 'hora', msg: 'Selecciona la hora.' },
    ];

    campos.forEach(({ id, msg }) => {
      const input = document.getElementById(id);
      const error = document.getElementById('err-' + id);
      if (!input || !input.value.trim()) {
        if (input) input.classList.add('error');
        if (error) { error.textContent = msg; error.classList.add('show'); }
        valid = false;
      } else {
        if (input) input.classList.remove('error');
        if (error) error.classList.remove('show');
      }
    });

    // Validar email
    const emailInput = document.getElementById('email');
    if (emailInput && emailInput.value && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailInput.value)) {
      emailInput.classList.add('error');
      const errEmail = document.getElementById('err-email');
      if (errEmail) { errEmail.textContent = 'Ingresa un correo válido.'; errEmail.classList.add('show'); }
      valid = false;
    }

    // Validar fecha no pasada
    const fechaInput = document.getElementById('fecha');
    if (fechaInput && fechaInput.value) {
      const hoy = new Date();
      hoy.setHours(0, 0, 0, 0);
      const seleccionada = new Date(fechaInput.value + 'T00:00:00');
      if (seleccionada < hoy) {
        fechaInput.classList.add('error');
        const errFecha = document.getElementById('err-fecha');
        if (errFecha) { errFecha.textContent = 'La fecha no puede ser en el pasado.'; errFecha.classList.add('show'); }
        valid = false;
      }
    }

    if (valid) {
      showToast('✅ Cita agendada exitosamente. Te contactaremos pronto.');
      citaForm.reset();
    }
  });

  // Limpiar error al escribir
  citaForm.querySelectorAll('input, select, textarea').forEach(el => {
    el.addEventListener('input', () => {
      el.classList.remove('error');
      const err = document.getElementById('err-' + el.id);
      if (err) err.classList.remove('show');
    });
  });

  // Cargar médicos según especialidad
  const especialidadSelect = document.getElementById('especialidad');
  const medicoSelect = document.getElementById('medico');

  const medicosPorEspecialidad = {
    'medicina-general': ['Dr. Andrés Gómez', 'Dra. Laura Herrera'],
    'pediatria': ['Dra. Sofía Ramírez', 'Dr. Carlos Medina'],
    'odontologia': ['Dr. Felipe Torres', 'Dra. Valentina Cruz'],
    'cardiologia': ['Dr. Roberto Nieto', 'Dra. Marcela Ríos'],
    'ginecologia': ['Dra. Patricia Lozano', 'Dra. Isabel Morales'],
    'traumatologia': ['Dr. Javier Pedraza', 'Dr. Santiago Vargas'],
  };

  if (especialidadSelect && medicoSelect) {
    especialidadSelect.addEventListener('change', () => {
      const val = especialidadSelect.value;
      medicoSelect.innerHTML = '<option value="">-- Seleccionar médico --</option>';
      if (medicosPorEspecialidad[val]) {
        medicosPorEspecialidad[val].forEach(m => {
          const opt = document.createElement('option');
          opt.value = m.toLowerCase().replace(/\s/g, '-');
          opt.textContent = m;
          medicoSelect.appendChild(opt);
        });
      }
    });
  }
}

// ===== FORMULARIO CONTACTO =====
const contactForm = document.getElementById('formContacto');
if (contactForm) {
  contactForm.addEventListener('submit', function (e) {
    e.preventDefault();
    let valid = true;

    [['nombre-c', 'Ingresa tu nombre.'], ['email-c', 'Ingresa tu correo.'], ['mensaje', 'Escribe tu mensaje.']].forEach(([id, msg]) => {
      const el = document.getElementById(id);
      const err = document.getElementById('err-' + id);
      if (!el || !el.value.trim()) {
        if (el) el.classList.add('error');
        if (err) { err.textContent = msg; err.classList.add('show'); }
        valid = false;
      } else {
        if (el) el.classList.remove('error');
        if (err) err.classList.remove('show');
      }
    });

    if (valid) {
      showToast('✅ Mensaje enviado. Te responderemos a la brevedad.');
      contactForm.reset();
    }
  });

  contactForm.querySelectorAll('input, textarea').forEach(el => {
    el.addEventListener('input', () => {
      el.classList.remove('error');
      const err = document.getElementById('err-' + el.id);
      if (err) err.classList.remove('show');
    });
  });
}

// ===== FECHA MINIMA EN INPUT DATE =====
const fechaInput = document.getElementById('fecha');
if (fechaInput) {
  const hoy = new Date().toISOString().split('T')[0];
  fechaInput.setAttribute('min', hoy);
}

// ===== ANIMACION CONTADORES STATS =====
function animateCounters() {
  document.querySelectorAll('.counter').forEach(el => {
    const target = parseInt(el.getAttribute('data-target'));
    const duration = 1500;
    const step = target / (duration / 16);
    let current = 0;
    const timer = setInterval(() => {
      current += step;
      if (current >= target) {
        el.textContent = target.toLocaleString() + (el.getAttribute('data-suffix') || '');
        clearInterval(timer);
      } else {
        el.textContent = Math.floor(current).toLocaleString() + (el.getAttribute('data-suffix') || '');
      }
    }, 16);
  });
}

// Observer para activar contadores cuando son visibles
const statsSection = document.querySelector('.stats');
if (statsSection) {
  const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        animateCounters();
        observer.disconnect();
      }
    });
  }, { threshold: 0.3 });
  observer.observe(statsSection);
}
