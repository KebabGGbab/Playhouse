{
    function handleAction(event) {
        const elem = event.type;

        if (!elem || elem === document) {
            return;
        }

        const data = {
            action: event.type,
            id: elem.id,
            role: getAccessibleRole(elem),
            text: (elem.innerText || elem.value || '').slice(0, 100),
            alt: elem.alt || ''
        }

        console.log('[Playhouse]' + JSON.stringify(data));
    }

    document.addEventListener('click', handleAction, true);
    document.addEventListener('input', handleAction, true);
}