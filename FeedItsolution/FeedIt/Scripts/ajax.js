$(function () {

    $('body').on('click', '#followersbutton', function () {
        $("#followings").addClass("hidden");
        $("#followers").removeClass("hidden");
    });

    $('body').on('click', '#followingbutton', function () {
        $("#followings").removeClass("hidden");
        $("#followers").addClass("hidden");
    });

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

    $('body').on('click', '#commentform', function () {
        var post = $('#postid').val();
        var comment = $('#content').val();

        var theForm = $(this);

        $.ajax({
            type: 'POST',
            url: '/Post/Comment',
            data: {
                postId: post,
                comment: comment
            }
        }).done(function (data) {
            console.log(data);

            $('#commentlist').prepend(
               '<blockquote class="Commentsection"> <div> <p>'
                     + data.comment.comment + '</p> <footer> - ' + data.user.UserName + ' just now ' + '</footer> </div> </blockquote>');
            $('#content').val('');
            return false;
        })
    });

    //Fail: myndir loadast ekki með ajaxi :'(
    /*$('body').on('click', '#makepostform', function () {
        var picture = $('#picture').val();
        var description = $('#description').val();

        var theForm = $(this);

        $.ajax({
            type: 'POST',
            url: '/Home/createPost',
            data: {
                picture: picture,
                description: description,
            }
        }).done(function (data) {
            
            $('#friendfeed').prepend(
                '<div class="wrapper"> <div class="navleft"> <div> <a href="'
                + '@Url.Action("Profile", "Profile", new { userID = ' + data.user.Id + '})">'
                + '<img src="@Url.Content(' + data.user.profilePicture + ')" alt="picture" class="feedprofilepic" />'
                + '</a> </div> <p class="Postuser"> <a href=@Url.Action("Profile", "Profile", new { userID = '
                + data.user.Id + ' })>' + data.user.UserName + '</a> </p> <p class="Posttime"> just now </p>'
                + '</div> <div class="Post"> <a href="@Url.Action("Index", "Post", new { id = ' + data.post.ID + '})">'
                + '<img src="@Url.Content(' + data.post.picture + ')" alt="picture" class="postpicture" /> </a>'
                + '<p class="Postabout">' + data.post.about + '</p> </div> <div class="navright"> </div> </div>');
            $('#allfeed').prepend(
                '<div class="wrapper"> <div class="navleft"> <div> <a href="'
                + '@Url.Action("Profile", "Profile", new { userID = ' + data.user.Id + '})">'
                + '<img src="@Url.Content(' + data.user.profilePicture + ')" alt="picture" class="feedprofilepic" />'
                + '</a> </div> <p class="Postuser"> <a href=@Url.Action("Profile", "Profile", new { userID = '
                + data.user.Id + ' })>' + data.user.UserName + '</a> </p> <p class="Posttime"> just now </p>'
                + '</div> <div class="Post"> <a href="@Url.Action("Index", "Post", new { id = ' + data.post.ID + '})">'
                + '<img src="@Url.Content(' + data.post.picture + ')" alt="picture" class="postpicture" /> </a>'
                + '<p class="Postabout">' + data.post.about + '</p> </div> <div class="navright"> </div> </div>');
            $('#picture').val('');
            $('#description').val('');
            return false;
        })
    });*/

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
                $('#currentrating').html(data.rating);
            }
        })
    });
});