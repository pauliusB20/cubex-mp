<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>@yield('title-block')</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <!-- csrf token-->
  <meta name="csrf-token" content="{{ csrf_token() }}">
  <!-- fa fa-->
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" integrity="sha384-UHRtZLI+pbxtHCWp1t77Bi1L4ZtiqrqD80Kn4Z8NTSRyMA2Fd33n5dQ8lWUE00s/" crossorigin="anonymous">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="{{ asset('plugins/font-awesome/css/font-awesome.min.css') }}">
  <!-- Icons -->
  <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="{{ asset('dist/css/adminPage.css') }}">
  <link rel="stylesheet" href="{{ asset('dist/css/adminlte.css') }}">
  <!-- iCheck -->
  <link rel="stylesheet" href="{{ asset('plugins/iCheck/flat/blue.css') }}">
  <!-- Morris chart -->
  <link rel="stylesheet" href="{{ asset('plugins/morris/morris.css') }}">
  <!-- Date Picker -->
  <link rel="stylesheet" href="{{ asset('plugins/datepicker/datepicker3.css') }}">
  <!-- Daterange picker -->
  <link rel="stylesheet" href="{{ asset('plugins/daterangepicker/daterangepicker-bs3.css') }}">
  <!-- bootstrap wysihtml5 - text editor -->
  <link rel="stylesheet" href="{{ asset('plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css') }}">
  <!-- Google Font: Source Sans Pro -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700">
  <!--Timer css -->
  <link rel="stylesheet" href="{{ asset('dist/css/jQuery.countdownTimer.css') }}">
  <!-- Toastr css-->
  <link  rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
  <!--Timer scripts -->
  <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script type="text/javascript" src="{{asset('dist/js/jQuery.countdownTimer.js')}}"></script>
  <!--Toastr notifications -->
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
  {{-- ChartScript --}}
  @isset($hdiagram)
    @if($hdiagram)
    {!! $hdiagram->script() !!}
    @endif
  @endisset
</head>
<body class="sidebar-mini layout-fixed" style="height: auto;">
  <div class="wrapper">
    <nav class="main-header navbar navbar-expand navbar-white navbar-light">
      <!-- Left navbar links -->
      <ul class="navbar-nav">
        <li class="nav-item">
          <a class="nav-link" data-widget="pushmenu" href="#" role="button"><i class="fas fa-bars"></i></a>
        </li>
      </ul>
      <!-- Right navbar links -->
      <ul class="navbar-nav ml-auto">
        <!-- Messages Dropdown Menu -->
        <li class="nav-item dropdown">
          @guest
          <li><a class="nav-link" href="{{ route('login') }}">{{ __('Login') }}</a></li>
          <li><a class="nav-link" href="{{ route('register') }}">{{ __('Register') }}</a></li>
          <li><a class="nav-link" href="/">{{ __('About') }}</a></li>
          @else
          <li class="nav-item dropdown">
            <a id="navbarDropdown" class="nav-link dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" v-pre>
              {{ Auth::user()->nickname }} <span class="caret"></span>
            </a>
              <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                <a class="dropdown-item" href="{{ route('logout') }}" onclick="event.preventDefault();
                                                      document.getElementById('logout-form').submit();">
                    {{ __('Logout') }}
                </a>
              <form id="logout-form" action="{{ route('logout') }}" method="POST" style="display: none;">
              @csrf
              </form>
              </div>
          </li>
          @endguest
        </li>
      </ul>
    </nav>
    <!-- Main Sidebar Container -->
    <aside class="main-sidebar sidebar-dark-primary elevation-4">
      <!-- Brand Logo -->
      <a href="#" class="brand-link">
        <img src="{{ asset('dist/img/AdminLTELogo.png') }}" alt="AdminLTE Logo" class="brand-image img-circle elevation-3" style="opacity: .8">
        <span class="brand-text font-weight-light">Cube Market</span>
      </a>
      <!-- Sidebar -->
      <div class="sidebar">
        <!-- Sidebar user panel (optional) -->
          <div class="user-panel mt-3 pb-3 mb-3 d-flex">
            <div class="image">
              <img src="{{ asset('dist/img/user2-160x160.jpg') }}" class="img-circle elevation-2" alt="User Image">
            </div>
            <div class="info">
              <a href="/account" class="d-block">My Account</a>
            </div>
          </div>
          <div class="user-panel mt-3 pb-3 mb-3 d-flex">
            <nav class="mt-2">
              <ul class="nav nav-pills nav-sidebar flex-column nav-balance" data-widget="treeview" role="menu" data-accordion="false">
                 <li style="color:white" class="nav-item">
                  <p>User Cubecoin amount</p>
                </li>
                 <li class="nav-item has-treeview">
                  <a href="#" class="nav-link">
                    <i class="nav-icon fa fa-btc"></i>
                    <p>Your balance
                    <i class="fas fa-angle-left right"></i>
                    </p>
                  </a>
                  <ul class="nav nav-treeview" style="display: none;">
                    <li class="nav-item cubecoin">
                    <script>
                        jQuery(document).ready(function(){
                          $.ajax({
                          type: 'GET',
                          url: "{{route('getBalance')}}",
                          data: {wallet_address : "{{env('ADMIN_ADDRESS')}}"},
                          success: function(data) {
                          $('.cubecoin').append("Your current cat.currency balance <br/> is : " + data.success);
                          },
                          error: function(data){
                          toastr.error('Something went wrong with loading data from your nem wallet, make sure you have one!!', 'Inconceivable!', {timeOut: 5000});
                          }
                          });
                        });
                      </script>
                    </li>
                    <li class = "nav-item totalEnergon">
                    <script>
                        jQuery(document).ready(function(){
                          $.ajax({
                          type: 'GET',
                          url: "{{route('getTotalEnergonBalance')}}",
                          success: function(data) {
                          $('.totalEnergon').append("Total amount of market energon <br/> is : " + data.success);
                          },
                          error: function(data){
                          toastr.error('Your balance is negative!! ' + data.error, 'Inconceivable!', {timeOut: 5000});
                          }
                          });
                        });
                    </script>
                    </li>
                    <li class = "nav-item totalCredits">
                    <script>
                       jQuery(document).ready(function(){
                          $.ajax({
                          type: 'GET',
                          url: "{{route('getTotalCreditsBalance')}}",
                          success: function(data) {
                          $('.totalCredits').append("Total amount of market credits <br/> is : " + data.success);
                          },
                          error: function(data){
                          toastr.error('Your balance is negative!! ' + data.error, 'Inconceivable!', {timeOut: 5000});
                          }
                          });
                        });
                    </script>
                    </li>
                  </ul>
                </li>
              </ul>
            </nav>
          </div>
        <!-- Sidebar Menu -->
        <nav class="mt-2">
          <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
            <li style="color:white" class="nav-item">
              <p>Administrator tools</p>
            </li>
            <!-- Add icons to the links using the .nav-icon class
                with font-awesome or any other icon font library -->
            <li class="nav-item">
              <a href="/market" class="nav-link
              ">
                <i class="nav-icon fa fa-dashboard"></i>
                <p>Items market</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="/marketResources" class="nav-link
              ">
                <i class="nav-icon fa fa-dashboard"></i>
                <p>Resource market items</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="{{ route('adminPage') }}" class="nav-link
              ">
                <i class="nav-icon fa fa-dashboard"></i>
                <p>Dashboard</p>
              </a>
            </li>
            <li class ="nav-item">
               <a href ="{{route('newspost')}}" class="nav-link">
               <i class="nav-icon fa fa-th"></i>
               <p>News posting tool</p>
               </a>
            </li>
            <li class ="nav-item">
               <a href ="{{route('postednews')}}" class="nav-link">
               <i class="nav-icon fa fa-th"></i>
               <p>News article viewer</p>
               </a>
            </li>
            <li class="nav-item">
              <a href="{{route('adminPageUDB')}}" class="nav-link
              ">
                <i class="nav-icon fa fa-th"></i>
                <p> User database</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="{{route('admtmot')}}" class="nav-link
              ">
                <i class="nav-icon fa fa-th"></i>
                <p>Resource transactions <br /> monitor</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="{{route('admtitem')}}" class="nav-link
              ">
                <i class="nav-icon fa fa-th"></i>
                <p>User game asset  <br /> transactions monitor</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="{{route('admh')}}" class="nav-link
              ">
                <i class="nav-icon fa fa-th"></i>
                <p>User login history</p>
              </a>
            </li>
          </ul>
        </nav>
        <!-- /.sidebar-menu -->
      </div>
      <!-- /.sidebar -->
    </aside>
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
      @yield('content')
    </div>
    <!-- /.content-wrapper -->
    <footer class="main-footer">
      <strong>Copyright &copy; 2020 <a href="http://adminlte.io">Cubex game industry</a>.</strong>
      All rights reserved.
        <div class="float-right d-none d-sm-inline-block">
          <b>Version</b> 1.0.0-alpha
        </div>
    </footer>
  </div>
  <!-- jQuery -->
  <script src="{{ asset('plugins/jquery/jquery.min.js') }}"></script>
  <script src="{{ asset('js/test.js') }}"> </script>
  {{-- <script src="{{ asset('dist/js/searchusers.js') }}"> </script> --}}
  <!-- jQuery UI 1.11.4 -->
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
  <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
  <script>
    $.widget.bridge('uibutton', $.ui.button)
  </script>
  <!-- Bootstrap 4 -->
  <script src="{{ asset('plugins/bootstrap/js/bootstrap.bundle.min.js') }}"></script>
  <!-- Morris.js charts -->
  <script src="https://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
  <script src="{{ asset('plugins/morris/morris.min.js') }}"></script>
  <!-- Sparkline -->
  <script src="{{ asset('plugins/sparkline/jquery.sparkline.min.js') }}"></script>
  <!-- jvectormap -->
  <script src="{{ asset('plugins/jvectormap/jquery-jvectormap-1.2.2.min.js') }}"></script>
  <script src="{{ asset('plugins/jvectormap/jquery-jvectormap-world-mill-en.js') }}"></script>
  <!-- jQuery Knob Chart -->
  <script src="{{ asset('plugins/knob/jquery.knob.js') }}"></script>
  <!-- daterangepicker -->
  <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.2/moment.min.js"></script>
  <script src="{{ asset('plugins/daterangepicker/daterangepicker.js') }}"></script>
  <!-- datepicker -->
  <script src="{{ asset('plugins/datepicker/bootstrap-datepicker.js') }}"></script>
  <!-- Bootstrap WYSIHTML5 -->
  <script src="{{ asset('plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js') }}"></script>
  <!-- Slimscroll -->
  <script src="{{ asset('plugins/slimScroll/jquery.slimscroll.min.js') }}"></script>
  <!-- FastClick -->
  <script src="{{ asset('plugins/fastclick/fastclick.js') }}"></script>
  <!-- AdminLTE App -->
  <script src="{{ asset('dist/js/adminlte.js') }}"></script>
  <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
  <script src="{{ asset('dist/js/pages/dashboard.js') }}"></script>
  <!-- AdminLTE for demo purposes -->
  <script src="{{ asset('dist/js/demo.js') }}"></script>
  <!-- Modals plugin-->
  <script src="{{ asset('plugins/bootstrap/js/bootstrap.js') }}"></script>

</body>
</html>
