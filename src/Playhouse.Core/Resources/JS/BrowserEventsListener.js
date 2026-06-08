{
    async function handleAction(event, details = null) {
        const element = event.target;

        if (!element || element === document) {
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
            selector: getUniqueCssSelector(element),
            value: element.value
        };

        await window.SendAction(data);
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

    function getUniqueCssSelector(element) {
        if (!element || element.nodeType !== 1) return '';

        // 1. Если есть ID, это лучший и самый короткий селектор
        if (element.id) {
            return `#${CSS.escape(element.id)}`;
        }

        const path = [];
        let current = element;

        while (current && current.nodeType === 1) {
            let selector = current.tagName.toLowerCase();

            // 2. Проверяем специальные атрибуты, которые часто делают элемент уникальным (хорошо для тестов/ботов)
            const testId = current.getAttribute('data-testid');
            const name = current.getAttribute('name');

            if (testId) {
                selector += `[data-testid="${CSS.escape(testId)}"]`;
                path.unshift(selector);
                break; // data-testid обычно уникален, можно останавливаться
            }
            if (name && (current.tagName.toLowerCase() === 'input' || current.tagName.toLowerCase() === 'select')) {
                selector += `[name="${CSS.escape(name)}"]`;
                // Не прерываем сразу, имя может повторяться (например, radio buttons), поэтому идем дальше, но этот атрибут усилит селектор
            }

            // 3. Если есть классы, добавляем первый (или можно добавить логику проверки уникальности класса)
            if (current.className && typeof current.className === 'string') {
                const classes = current.className.trim().split(/\s+/).filter(c => c && !c.startsWith('ng-') && !c.startsWith('_')); // Фильтруем фреймворчные классы
                if (classes.length > 0) {
                    selector += `.${CSS.escape(classes[0])}`;
                }
            }

            // 4. Fallback: добавляем порядковый номер среди соседей того же тега
            let sibling = current;
            let nth = 1;
            while (sibling.previousElementSibling) {
                sibling = sibling.previousElementSibling;
                if (sibling.tagName === current.tagName) nth++;
            }
            selector += `:nth-of-type(${nth})`;

            path.unshift(selector);
            current = current.parentElement;

            // Останавливаемся на body, чтобы селектор не был бесконечным
            if (current && current.tagName.toLowerCase() === 'body') {
                path.unshift('body');
                break;
            }
        }

        return path.join(' > ');
    }

    document.addEventListener('click', handleAction, true);
    document.addEventListener('change', handleAction, true);
}