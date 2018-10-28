(function () {
    function favoriteGuide() {
        const favoriteButton = $($('.favorite')[0]);
        const guideIdInput = $($('.guide-id')[0]);

        if (!(favoriteButton && guideIdInput)) {
            return;
        }

        let isButtonEnabled = true;
        favoriteButton.on('click', async () => {
            if (isButtonEnabled) {
                isButtonEnabled = false;
                console.log({ id: guideIdInput.val() });
                console.log(JSON.stringify({ id: guideIdInput.val() }));
                $.ajax({
                    method: 'POST',
                    data: JSON.stringify({ id: guideIdInput.val() }),
                    contentType: 'application/json',
                    url: '/guides/favorite'
                }).done(() => {
                    toastr.success("Success!");
                    favoriteButton.toggleClass('far');
                    favoriteButton.toggleClass('fas');
                }).fail(() => {
                    toastr.error("Ooops, something went wrong. Try again later")
                })
            }

            isButtonEnabled = true;
        })
    }

    favoriteGuide();
}())