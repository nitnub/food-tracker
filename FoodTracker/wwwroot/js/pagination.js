
function getPaginationDiv(query, page, totalPages) {

    page = Number(page);
    totalPages = Number(totalPages);

    if (totalPages == 0)
        return;
   
    const maxPages = Math.min(totalPages, 100);
    const ul = document.createElement('ul');
    ul.className = 'pagination';

    let innerList = prev(query, page);
    innerList += getLi(query, 1, page)

    // if short list...
    if (maxPages <= 7) {
        for (i = 2; i < maxPages; i++) {
            innerList += getLi(query, i, page);
        }
    }

    // else if current page <= 3...
    else if (page <= 3) {
        for (i = 2; i <= 4; i++) {
            innerList += getLi(query, i, page);
        }
        innerList += ellipsis();
    }

    // else if current page is within last 3 pages...
    else if (maxPages - page <= 2) {
        innerList += ellipsis();
        for (i = maxPages - 3; i < maxPages; i++) {
            innerList += getLi(query, i, page);
        }
    }

    // else if curent page is in middle
    else {
        innerList += ellipsis();
        for (i = page - 1; i <= page + 1; i++) {
            innerList += getLi(query, i, page);
        }
        innerList += ellipsis();
    }

    innerList += getLi(query, maxPages, page); 
    innerList += next(query, page, maxPages);
    ul.innerHTML = innerList;

    const nav = document.createElement('nav');
    nav.appendChild(ul);
    nav.ariaLabel = "...";
    return nav;
}

function getLi(query, iterator, pageNumber) {
    if (iterator == pageNumber) {
        return focus(query, iterator);
    }
    return option(query, iterator);
}

function focus(query, page) {
    return `
        <li class="page-item active" aria-current="page">
            <a class="page-link" onclick="getProducts('${query}', ${page})" href="#">${page}</a>
        </li>`
}

function option(query, page) {
    return `
        <li class="page-item">
            <a class="page-link" onclick="getProducts('${query}', ${page})" href="#">${page}</a>
        </li>`
}

function ellipsis() {
    return `<li class="page-item"><a class="page-link">...</a></li>`
}

function prev(query, page) {
    return `
        <li class="page-item ${page == 1 && 'disabled'}">
            <a class="page-link" onclick="getProducts('${query}', ${page - 1})" href="#">Previous</a>
        </li>`
}

function next(query, page, maxPages) {
    return `
        <li class="page-item ${page == maxPages && 'disabled'}">
            <a class="page-link" onclick="getProducts('${query}', ${page + 1})" href="#">Next</a>
        </li>`
}
