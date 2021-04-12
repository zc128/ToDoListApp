const cacheName = 'v1';

function swInstall(event) {
    // Perform install steps
    console.info('Service Worker Installing...');
    event.waitUntil(
        caches.open('v1').then((cache) => {
            return cache.addAll([
                './',
                './css/site.css',
                './js/site.js'
            ]);
        })
    );
    console.info('Finished Caching Website...');
}
self.addEventListener('install', swInstall);


//Cache First
self.addEventListener('fetch', function (event) {
    event.respondWith(
        caches.match(event.request).then(function (cacheResponse) {
            if (cacheResponse) {
                return cacheResponse;
            } else {
                return fetch(event.request).then((netResp) => netResp)
            }
        })
    )
})
