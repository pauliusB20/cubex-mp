<!DOCTYPE html>
<html lang="en">
<style>
    html,
    body {
        background-color: #fff;
        color: #636b6f;
        font-family: 'Nunito', sans-serif;
        font-weight: 200;
        height: 100vh;
        margin: 0;
    }

    .full-height {
        height: 100vh;
    }

    .flex-center {
        align-items: center;
        display: flex;
        justify-content: center;
    }

    .position-ref {
        position: relative;
    }

    .top-right {
        position: absolute;
        right: 10px;
        top: 18px;
    }

    .content {
        text-align: center;
    }

    .title {
        font-size: 84px;
    }

    .links>a {
        color: #636b6f;
        padding: 0 25px;
        font-size: 13px;
        font-weight: 600;
        letter-spacing: .1rem;
        text-decoration: none;
        text-transform: uppercase;
    }

    .m-b-md {
        margin-bottom: 30px;
    }

    .button {
        display: inline-block;
        border-radius: 4px;
        background-color: #f4511e;
        border: none;
        color: #FFFFFF;
        text-align: center;
        font-size: 28px;
        padding: 20px;
        width: 200px;
        transition: all 0.5s;
        cursor: pointer;
        margin: 5px;
    }

    .button span {
        cursor: pointer;
        display: inline-block;
        position: relative;
        transition: 0.5s;
    }

    .button span:after {
        content: '\00bb';
        position: absolute;
        opacity: 0;
        top: 0;
        right: -20px;
        transition: 0.5s;
    }

    .button:hover span {
        padding-right: 25px;
    }

    .button:hover span:after {
        opacity: 1;
        right: 0;
    }

    /* Gray */
    .default {
        color: black;
        text-align: center;
    }

    .default:hover {
        background: #e7e7e7;
    }
</style>

<body>
    <div class="ct-pageWrapper" id="ct-js-wrapper">
        <section class="company-heading intro-type" id="parallax-one">
            <div class="container">
                <div class="row product-title-info">
                    <div class="col-md-12">
                        <h1>About Us</h1>
                    </div>
                </div>
            </div>
            <div class="parallax" id="parallax-cta" style="background-image:url(https://www.solodev.com/assets/hero/hero.jpg);"></div>
        </section>
        <section class="story-section company-sections ct-u-paddingBoth100 paddingBothHalf noTopMobilePadding" id="section">
            <div class="container text-center">
                <h2>Cube Market?</h2>
                <h3>Here you can buy and sell items that are featured in the RTS Game "Cubex"</h3>
                <h3>Click on the market button to view the available items or login to your account</h3>
            </div>
        </section>
    </div>
    </div>
    </div>
    </main>
    <div>
        @if (Route::has('login'))
        <div style="text-align:center;">
            @auth
            <h2>You are logged in!</h2>
            <a href="/account" class="btn default" style="font-size: 33px; font-weight: 600;">Enter</a>
            @else
            <a href="/market" class="btn default" style="font-size: 33px; font-weight: 600;">Market items</a>
            <a href="/marketResources" class="btn default" style="font-size: 33px; font-weight: 600;">Resource market items</a>
            <a href="{{ route('login') }}" class="btn default" style="font-size: 33px; font-weight: 600;">Login</a>
            @if (Route::has('register'))
            <a href="{{ route('register') }}" class="btn default" style="font-size: 33px; font-weight: 600;">Register</a>
            @endif
            @endauth
            </button>
        </div>
    </div>
    @endif
    </div>
</body>

</html>

<link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css">
<link rel="stylesheet" href="about-us.css">
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet">
<script src="https://code.jquery.com/jquery-2.2.0.min.js" type="text/javascript"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
