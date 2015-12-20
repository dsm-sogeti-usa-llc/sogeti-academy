var gulp = require('gulp');
var shell = require('gulp-shell');
var concat = require('gulp-concat');
var watch = require('gulp-watch');
var path = require('path');

var assets = {
    jsdest: path.join(__dirname, 'wwwroot', 'js'),
    js: [
        path.join(__dirname, 'node_modules', 'jquery', 'dist', 'jquery.min.js'),
        path.join(__dirname, 'node_modules', 'bootstrap', 'dist', 'js', 'bootstrap.min.js'),
        path.join(__dirname, 'Presentation', '**/*.js')
    ],
    cssdest: path.join(__dirname, 'wwwroot', 'css'),
    css: [
        path.join(__dirname, 'node_modules', 'bootstrap', 'dist', 'css', 'bootstrap.min.css'),
        path.join(__dirname, 'node_modules', 'bootstrap', 'dist', 'css', 'bootstrap-theme.min.css'),
        path.join(__dirname, 'Presentation', '**/*.css')
    ],
    fontsdest: path.join(__dirname, 'wwwroot', 'fonts'),
    fonts: [
        path.join(__dirname, 'node_modules', 'bootstrap', 'dist', 'fonts', '*.eot'),
        path.join(__dirname, 'node_modules', 'bootstrap', 'dist', 'fonts', '*.svg'),
        path.join(__dirname, 'node_modules', 'bootstrap', 'dist', 'fonts', '*.ttf'),
        path.join(__dirname, 'node_modules', 'bootstrap', 'dist', 'fonts', '*.woff'),
        path.join(__dirname, 'node_modules', 'bootstrap', 'dist', 'fonts', '*.woff2')
    ]
};

gulp.task('build:js', function () {
    return gulp.src(assets.js)
        .pipe(concat('app.js'))
        .pipe(gulp.dest(assets.jsdest));
});

gulp.task('build:css', function(){
    return gulp.src(assets.css)
        .pipe(concat('styles.css'))
        .pipe(gulp.dest(assets.cssdest));
});

gulp.task('build:fonts', function(){
    return gulp.src(assets.fonts)
        .pipe(gulp.dest(assets.fontsdest));
});

gulp.task('build:dnx', shell.task(['dnu publish']));

gulp.task('build', ['build:js', 'build:css', 'build:fonts', 'build:dnx']);