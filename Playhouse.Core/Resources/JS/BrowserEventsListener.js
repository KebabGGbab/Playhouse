document.addEventListener('click', e => {
    const selector = e.target?.outerHTML?.slice(0, 200);
    console.log('[Playhouse]:!:click:!::!:' + selector);
});
document.addEventListener('input', e => {
    const selector = e.target?.outerHTML?.slice(0, 200);
    console.log('[Playhouse]:!:input:!::!:' + selector + ' => ' + e.target.value);
});