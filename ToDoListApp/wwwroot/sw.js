const cacheName = 'v2';

function swInstall(event) {
    // Perform install steps
    console.info('Service Worker Installing...');
    event.waitUntil(
        caches.open('v2').then((cache) => {
            return cache.addAll([
                './',
                './home',
                './css/site.css',
                './js/site.js',
                './favicon.ico'
            ]);
        })
    );
    console.info('Finished Caching Website...');
}
self.addEventListener('install', swInstall);

//NetworkFirst
self.addEventListener('fetch', function (event) {
    event.respondWith(
        fetch(event.request)
            .then(function (networkResponse) {
                caches.open('v2').then((cache) => {
                    cache.add(event.request.url);
                });
                return networkResponse
            }).catch(function () {
                return caches.match(event.request)
            })
    )
})