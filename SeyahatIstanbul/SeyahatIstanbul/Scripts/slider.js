$(function () {

    var Slider = function (slider) {
        this.sl = slider;
        this.mevcut = 0;
        this.toplam = slider.find('.slide').length;

        this.kaydir = function (konum) {
            if ((konum + 1) > this.toplam) {
                // Son slidesa başa dön
                konum = 0;
            } else if (konum < 0) {
                // En baştaysa sona
                konum = this.toplam - 1;
            }

            // Slider kaç px kayacak
            // CSS'teki sliderın genişliğine göre değişecek px boyutu. 960 yerine size uygun olanı yazın.
            var kay = 960 * konum;

            // Kaydır
            $('.slider-container', this.sl).animate({
                marginLeft: '-' + kay + 'px'
            }, 900);

            // Mevcut durumu kaydet
            this.mevcut = konum;
        };

        this.ileri = function () {
            this.kaydir(this.mevcut + 1);
        };

        this.geri = function () {
            this.kaydir(this.mevcut - 1);
        };

        var slideIns = this;

        // İleri geri butonlarını ata
        $('.slider-ok-sag', this.sl).click(function (event) {
            event.preventDefault();

            slideIns.ileri();
        });

        $('.slider-ok-sol', this.sl).click(function (event) {
            event.preventDefault();

            slideIns.geri();
        });


    };
});