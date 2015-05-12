$(function () {

    // Ágætt að venja sig á að nota "event delegation", þ.e .on() fallið í jQuery, en það hengir event á það sem er
    // akkúrat núna í DOM trénu í skjalinu og *einnig* öll element sem gætu bæst við síðar á dýnamískan máta
    // Hér hengjum við submit event á öll form í skjalinu (sem er bara eitt eins og er)
    /*$('body').on('submit', '#reviewform', function () {
        // Inn í þessum submit event handler er $(this) vísun í form tagið sjálft.  Geymum reference á það í breytunni "theForm"
        var theForm = $(this);

        // Framkvæmum okkar eigin Async POST aðgerð (AJAX) með jQuery .ajax() fallinu
        $.ajax({
            type: 'POST',
            url: theForm.attr('action'), // Hér harðkóðum við url-ið svo það bendi á AjaxHomeController og AddComment aðgerðina þar
            data: theForm.serialize(), // .serialize() aðgerðin les allar upplýsingar úr forminu og býr til query-string úr því, þ.e &name1=value1&name2=value2 etc.
        }).done(function (result) {

            console.log(result);

            // Hér þarf aðeins meira föndur til að uppfæra view-ið með nýju gögnunum.

            // Byrjum á því að uppfæra fjölda commenta.
            // Gögnin sem við fáum til baka er listi af öllum commentum, sem er í raun array í Javascript og því getum við notað .length eigindið til að fá fjöldann.
            // Við finnum svo span tagið sem er með class="badge" og er undir comments-list div-inu, og uppfærum textann í því:

            // Fjarlægjum öll núverandi komment, sem eru öll blockquote tögin sem eru inn í divinu með id = comments-list
            $('#review-list blockquote').remove();

            // Loopum í gegnum result og bætum aftur við commentum:
            for (var i = 0; i < result.Reviews.length; i++) {
                //console.log(result.Reviews[i].CreatedDate);
                $('#review-list').append('<blockquote>' +
							'<p>' + result.Reviews[i].Text + '</p>' +
							'<footer>' + result.Reviews[i].Username + ' - ' + '</footer>' +
					'</blockquote>');
            }

            // Loks þurfum við að tæma innsláttarreitinn, því hann er ekki hluti af því HTML-i sem við erum að uppfæra.
            theForm.find('#reviewtext').val('');
        }).fail(function () {
            alert('Villa kom upp!');
        });

        // Verðum að muna að gera return false til að koma í veg fyrri að vafrinn framkvæmi default aðgerðina þegar
        // smellt er á submit hnappinn, en það er einmitt að submit-a forminu sem veldur því að öll síðan endurhleðst.
        // Það er einmitt það sem við erum að reyna að koma í veg fyrir að gerist.
        return false;
    });*/

    $('body').on('click', '#friendsfeedbutton', function () {
        $("#groupfeed").addClass("hidden");
        $("#allfeed").addClass("hidden");
        $("#friendfeed").removeClass("hidden");
    });

    $('body').on('click', '#groupfeedbutton', function () {
        $("#groupfeed").removeClass("hidden");
        $("#allfeed").addClass("hidden");
        $("#friendfeed").addClass("hidden");
    });

    $('body').on('click', '#allfeedbutton', function () {
        $("#groupfeed").addClass("hidden");
        $("#allfeed").removeClass("hidden");
        $("#friendfeed").addClass("hidden");
    });


    $('body').on('click', '#rateform', function () {
        var post = $('#postid').val();
        var rate = $('#rateinfo:checked').val();

        var theForm = $(this);

        $.ajax({
            type: 'POST',
            url: '/Post/ratePost',
            data: {
                postId: post,
                rateinfo: rate
            }
        }).done(function (data) {
            if (data == "") { }
            else {
                //$('#rating').html(data.result.RatingOverall);
                $('#currentrating').html(data.rating);
                //$('#yourrating').html('I rated ' + rate + ' Stars');
                /*for(var i = 1; i <= rate; i++)
	            {
	                $(".star").removeClass("golden");
	            }
	            for(var i = rate + 1; i < 11; i++)
	            {
	                $(".star").addClass("golden");
	            }*/
            }
        })
    });
});