{
    async function HandleLocatorAction(event) {
        const element = event.target;

        if (element instanceof HTMLElement == false) {
            return;
        }

        let text = (element.innerText || element.alt || element.placeholder || element.getAttribute('aria-label'));
        if (text != null) {
            text = text.slice(0, 100);
        }

        const data = {
            action: event.type,
            id: element.id,
            role: getRole(element),
            text: text,
            selector: getCssSelector(element),
            value: element.value
        };

        await window.SendLocatorAction(data);
    }

    function getRole(element) {
        if (!element || element.nodeType !== 1) {
            return null;
        }

        const explicitRole = element.getAttribute('role');
        if (explicitRole) {
            return explicitRole.toLowerCase().trim();
        }

        const implicitRoles = {
            'a': 'link',
            'button': 'button',
            'input': element.type === 'checkbox' ? 'checkbox' :
                     element.type === 'radio' ? 'radio' :
                     element.type === 'submit' ? 'button' : 'textbox',
            'select': 'combobox',
            'textarea': 'textbox',
            'img': 'img',
            'form': 'form',
            'main': 'main',
            'nav': 'navigation',
            'article': 'article',
            'section': 'region',
            'h1': 'heading', 'h2': 'heading', 'h3': 'heading',
            'h4': 'heading', 'h5': 'heading', 'h6': 'heading',
            'table': 'table',
            'ul': 'list', 'ol': 'list'
        };
        const tagName = element.tagName.toLowerCase();

        if (implicitRoles[tagName]) {
            return implicitRoles[tagName];
        }
        else {
            return null;
        }
    }

    function getCssSelector(element) {
        if (element instanceof HTMLElement == false) {
            return;
        }

        const path = [];
        let current = element;

        do {
            if (current.id) {
                path.unshift(`#${CSS.escape(current.id)}`);

                break;
            }

            const currentNodeName = current.nodeName.toLowerCase();
            const childrenParent = Array.from(current.parentElement.children);

            if (childrenParent.filter(item => item.nodeName == current.nodeName).length > 1) {
                path.unshift(`${currentNodeName}:nth-child(${childrenParent.indexOf(current) + 1})`);
            }
            else {
                path.unshift(currentNodeName);
            }

            current = current.parentElement;

        } while (current.parentElement !== null)

        return path.join(' > ');
    }

    async function DOMLoaded() {
        const href = document.URL;
        const referrer = document.referrer;

        if (href.startsWith('chrome') || referrer) {
            return;
        }

        const data = {
            referrer: referrer,
            href: href
        };

        await window.SendPageAction(data);
    }

    document.addEventListener('click', HandleLocatorAction, true);
    document.addEventListener('change', HandleLocatorAction, true);
    document.addEventListener('DOMContentLoaded', DOMLoaded)
}