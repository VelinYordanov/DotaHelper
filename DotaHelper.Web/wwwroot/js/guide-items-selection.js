(function () {
    function addItems() {
        const allItems = $($('.guide-items')[0]),
            selectedItems = $($('.selected-items')[0]);

        if (!(allItems.length && selectedItems.length)) {
            return;
        }

        allItems.on('click', event => {
            if (selectedItems.children().length >= 6) {
                return;
            }

            const target = $(event.target);
            const parent = target.parent('.guide-item');
            if (parent) {
                const elementToAdd = parent.clone(true);
                elementToAdd.children('.dota-item').first().hide();
                selectedItems.append(elementToAdd);
            }
        })

        selectedItems.on('click', event => {
            const target = $(event.target);
            const parent = target.parent('.guide-item');
            if (parent) {
                parent.remove();
            }
        })
    }

    function createGuide() {
        console.log('hereee');
        const createGuideForm = $('#create-guide-form'),
            selectedItems = $($('.selected-items')[0]),
            submitButton = $('#guide-submit-button');

        console.log(createGuideForm.length);
        if (!(createGuideForm.length && selectedItems.length)) {
            return;
        }
        
        submitButton.on('click', event => {
            event.preventDefault();
            console.log('submit');
            const childrenElements = selectedItems.children();
            if (childrenElements.length != 6) {
                toastr.error(`Only ${childrenElements.length} items selected! Add full 6 items!`)
                return;
            }
            console.log('asd');
            for (let i = 0; i < childrenElements.length; i += 1) {
                
                const input = $(childrenElements[i]).children('input[type=hidden]').first();
                console.log(input);
                const inputToAdd = input.clone();
                inputToAdd.attr('name', ('item' + (i+1)));
                createGuideForm.prepend(inputToAdd);
            }

            createGuideForm.submit();
        })
    }

    addItems();
    createGuide();
}())