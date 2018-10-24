function showItems() {
    console.log('here');
    const itemsImages = $('.dota-items');
    const images = $('.dota-item-image');

    if (!(itemsImages && images)) {
        return;
    }

    console.log(itemsImages);

    //itemsImages.on('click', event => {
    //    console.log('here4');
    //    const target = $(event.target);
    //    console.log(target);
    //    if (target.hasClass('dota-item-image')) {
    //        const itemDiv = target.prev('div');
    //        console.log(itemDiv);
    //        itemDiv.show();
    //    }
    //})

    images.hover(event => {
        console.log('here4');
        const target = $(event.target);
        console.log(target);
        const itemDiv = target.prev('div');
        console.log(itemDiv);
        itemDiv.show();
    }, event => {
        console.log('here5');
        const target = $(event.target);
        console.log(target);

        const itemDiv = target.prev('div');
        console.log(itemDiv);
        itemDiv.hide();
    })

for (let i = 0; i < itemsImages.length; i += 1) {
    console.log(itemsImages[i]);
}
    //itemsImages.each((index, element) => {
    //    console.log(element);
    //    element.on('mouseover', event => {
    //        console.log('here4');
    //        const target = $(event.target);
    //        if (target.hasClass('dota-item-image')) {
    //            const itemDiv = target.prev('div');
    //            itemDiv.show();
    //        }
    //    })
    //})
}

showItems();