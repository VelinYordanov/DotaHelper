function showItems() {
    const itemsImages = $('.dota-items');
    const images = $('.dota-item-image');

    if (!(itemsImages && images)) {
        return;
    }

    images.hover(event => {
        const target = $(event.target);
        const itemDiv = target.next('div');
        const x = event.clientX;
        const y = event.clientY;
        const itemDivWidth = itemDiv.outerWidth();
        const itemDivHalfHeight = itemDiv.height() / 2;
        itemDiv.css({ top: (y - itemDivHalfHeight) + 'px', left: (x - itemDivWidth) + 'px' });
        itemDiv.show();
    }, event => {
        const target = $(event.target);
        const itemDiv = target.next('div');
        itemDiv.hide();
    })
}

showItems();