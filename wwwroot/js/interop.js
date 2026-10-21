/*
 * interop.js — Módulo ES6 de JS Interop para TechMundo (Unidad 2, Clase 13).
 *
 * Se consume con el patrón recomendado por la cátedra: import() dinámico +
 * IJSObjectReference + liberación con IAsyncDisposable. NO se expone nada en
 * `window`: cada función se importa como módulo en el componente que la usa
 * (CatalogoLayout, AdminLayout, ProductoCard, Admin/Dashboard).
 */

/** Alterna el modo oscuro y lo persiste en localStorage. Devuelve el estado resultante. */
export function toggleModoOscuro() {
    document.body.classList.toggle('dark-mode');
    const activo = document.body.classList.contains('dark-mode');
    localStorage.setItem('modoOscuro', activo ? '1' : '0');
    return activo;
}

/** Aplica el modo oscuro guardado al montar el layout. Devuelve el estado aplicado. */
export function aplicarModoOscuroGuardado() {
    const activo = localStorage.getItem('modoOscuro') === '1';
    if (activo) document.body.classList.add('dark-mode');
    return activo;
}

/** Copia texto (ej. nombre de producto) al portapapeles. */
export function copiarAlPortapapeles(texto) {
    return navigator.clipboard.writeText(texto);
}

/** Notificación del navegador (ej. alerta de stock bajo al entrar al dashboard). */
export function mostrarNotificacion(titulo, mensaje) {
    if (!('Notification' in window)) return;

    if (Notification.permission === 'granted') {
        new Notification(titulo, { body: mensaje });
    } else if (Notification.permission !== 'denied') {
        Notification.requestPermission().then(permiso => {
            if (permiso === 'granted') new Notification(titulo, { body: mensaje });
        });
    }
}
