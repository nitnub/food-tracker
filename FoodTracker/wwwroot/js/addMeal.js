
const REACTION_LABEL_DEFAULT = 'Select';
let unitOptions;
let foodOptions;
let takenFoods = [];

function createUnitOptions(unitJson) {
    let output = "";
    unitJson.forEach(u => output += `<option value="${u.id}">${u.namePlural}</option>`);
    return output;
}
function createFoodOptions(foodJson) {
    let output = "";
    const fsa = $('.mi-food');

    if (fsa.length > 0) {
        fsa.each(function (i) { takenFoods.push(this.value); });
    }

    foodJson.forEach(f => {
        if (takenFoods.indexOf(f.id.toString()) < 0) {
            output += `<option value="${f.id}">${f.name}</option>`
        } else {
            output += `<option value="${f.id}" hidden disabled>${f.name}</option>`
        }
    });

    return output;
}

function addMealItem() {
    foodOptions = createFoodOptions(foodJson);

    const newId = new Date().getTime().toString();
    const mealGroup = document.getElementById('mealGroup')
    const div = document.createElement("div");

    div.setAttribute("class", "border form-group rounded-2 my-2 shadow p-2 remove-meal-item-new" + newId);

    div.innerHTML = `
        <div class="d-flex" >
            <div class="form-floating py-2 col-6">
                <select id="nmiFood_${newId}" class="form-select food-input mi-food" data-val="true" data-val-required="The FoodId field is required." id="MealItems_${newId}__FoodId" name="MealItems[${newId}].FoodId" placeholder="Meal Item">
                     ${foodOptions}
                </select>
                <label class="ms-2">Meal Item</label>
            </div>
            <div class="d-flex">
                <div class="form-floating p-2 col-6">
                    <input id="nmiVolume_${newId}" type="number" required min="1" value="1" class="form-control food-input mi-volume" type="text" data-val="true" data-val-number="The field Volume must be a number." data-val-required="The Volume field is required." id="MealItems_${newId}__Volume" name="MealItems[${newId}].Volume" value="" placeholder="Volume" >
                    <label class="ms-2" for="MealItems_${newId}__Volume">Volume</label>
                  <span class="text-danger field-validation-valid" data-valmsg-for="MealItems[${newId}].Volume" data-valmsg-replace="true"></span>
                </div>
                <div class="form-floating py-2 col-6">
                    <select id="nmiUnits_${newId}" class="form-select food-input mi-units" data-val="true" data-val-required="The Volume field is required." id="MealItems_${newId}__VolumeUnitsId" name="MealItems[${newId}].VolumeUnitsId" placeholder="Units">
                         ${unitOptions}
                    </select>
                    <label class="ms-2">Units</label>
                </div>
            </div>
        </div> 
        <div style="display: flex; justify-content:end">
            <a class="link-light" type="button" onclick="removeNewMealItem(${newId})">Remove</a>
        </div>`

    mealGroup.appendChild(div)

    $(`#nmiUnits_${newId}`).on('change', function () {
        $(`#nmiUnits_${newId}`).valid();
    });

    addDynamicFoodSelect(newId);
}

function addDynamicFoodSelect(newId) {
    let previous_value;
    let current_value;

    $(`#nmiFood_${newId}`).on('click', function (e) {
        previous_value = $(this)[0].value;
    }).on('change', function () {
        current_value = $(this).val()
        makeMealItemFoodVisible(previous_value);

        // hide new selection
        const tar = $('.mi-food').find($(`option[value='${current_value}']`));
        tar.each(function () {
            this.hidden = true;
        })
    });
}

$('.calendar-meal').each(function (e) {
    this.addEventListener('click', e => e.stopPropagation())
})


function activateMealModal(dayObj, activeMealId = 0) {
    $('.meal-details-modal').modal('show');
    $('.modal-title').html(dayObj);

    dayObj.activeMealId = activeMealId;
    getMeal(dayObj);
}

function getMeal(dayObj) {
    $.ajax({
        url: `/Guest/Meal/UpsertMeal`,
        type: 'POST',
        data: JSON.stringify(dayObj),
        contentType: 'application/json',
        success: function (data) {
            if (data) {
                populateMealCard(data);
                addTemplateListener();
            }
        }
    })
}

function populateMealCard(data) {
    // Populate meal card
    $(`#mealCard`).html(data);

    // Populate new meal item dropdowns
    takenFoods.length = 0;
    unitOptions = createUnitOptions(unitJson);
    foodOptions = createFoodOptions(foodJson);

    // Monitor and initialize meal reactions
    monitorMealReactions();
    initializeMealReactions(activeMealReactionsTest);

    // Mark all meal item food dropdowns as dynamic
    monitorExistingMealItems(mealItemCount);

    //addTemplateListener();
}

function monitorExistingMealItems(mealItemCount) {
    foodOptions = createFoodOptions(foodJson);
    for (let i = 0; i < mealItemCount; i++) {
        addDynamicFoodSelect(i);
    }
}


function initializeMealReactions(startingReactions) {
    clearActiveMealReactions();

    const activeChecks = Object.keys(startingReactions);

    activeChecks.forEach(reactionId => {
        $(`#reactionCheck-${reactionId}`).trigger("click");
    })
}

let activeMealReactions = [];
let currentLabel;

function monitorMealReactions() {
    $("#mealReactionResults").parents("div").find("li").on('change', function () {
        updateReactionSelectButton(this);
    })
}


function updateReactionSelectButton(ctx) {
    const value = $(ctx).attr("value");
    const checked = $(`#reactionCheck-${value}`)[0].checked
    const label = ctx.innerText.trim();

    if (checked) {
        activeMealReactions.push(label);
        addMealReaction(value);
    } else {
        const index = activeMealReactions.indexOf(label);
        activeMealReactions.splice(index, 1);
        removeMealReaction(value);
    }

    let subArray;
    let finalIndex = 4;

    for (let i = 0; i < activeMealReactions.length; i++) {
        subArray = activeMealReactions.slice(0, i);

        if (getFormattedArrayLength(subArray) > 25 || i == 3) {
            finalIndex = i - 1;
            break;
        }
    }

    if (activeMealReactions.length == 0) {
        currentLabel = REACTION_LABEL_DEFAULT;
    } else if (finalIndex <= 4) {
        currentLabel = activeMealReactions.slice(0, finalIndex).join(', ');
    } else if (activeMealReactions.length < 4) {
        currentLabel = activeMealReactions.join(', ');
    } else {
        currentLabel = activeMealReactions.slice(0, 4).join(', ');
    }

    if ((activeMealReactions.length - finalIndex) > 0) {
        currentLabel += ` (+${activeMealReactions.length - finalIndex} more)`;
    }

    $('#reactionDropdownSelect').val(currentLabel);
}

function clearActiveMealReactions() {
    activeMealReactions.length = 0;
    currentLabel = REACTION_LABEL_DEFAULT;
}

function getFormattedArrayLength(arr) {
    return arr.join(', ').length;
}

function addMealReaction(reactionId) {
    const mealReactionResults = document.getElementById('mealReactionResults')
    const div = document.createElement("div");

    div.setAttribute("id", "remove-meal-reaction-" + reactionId);
    div.innerHTML = `<input id="Reactions_${reactionId}" name="Reactions[${reactionId}]" value="true">`

    mealReactionResults.appendChild(div)
}

function removeMealReaction(reactionId) {
    $('#remove-meal-reaction-' + reactionId).remove();
}

function removeMeal(id) {
    $.ajax({
        url: `/Guest/Meal/RemoveMeal/${id}`,
        type: 'DELETE',
        contentType: 'application/json',
        success: function () {
            $(`#meal${id}`).remove();
            $('#meal-delete-modal').modal('hide');
        }
    })
}

function cancelRemoveMeal() {
    $('#meal-delete-modal').modal('hide');
    $('#meal-modal').modal('show');
}

function removeMealConfirmation(id, mealName) {
    $('#meal-modal').modal('hide');
    $('#meal-delete-modal-body').html(`Permanently delete "<b>${mealName}</b>"?`);
    $('#meal-delete-modal-footer').html(`
        <a onClick=cancelRemoveMeal() class="btn btn-secondary mx-2">Cancel</a>
        <a onClick=removeMeal(${id}) class="btn btn-danger mx-2">Delete</a>`);
    $('#meal-delete-modal').modal('show');
}

function removeNewMealItem(mealId) {
    // get food item falue
    const div = $('.remove-meal-item-new' + mealId);
    const value = div.find('select').first()[0].value;

    // remove the food from the "taken" list
    takenFoods = takenFoods.filter(f => f != value.toString());

    // make visible in all other dropdowns
    makeMealItemFoodVisible(value);

    // remove meal item row
    $('.remove-meal-item-new' + mealId).remove();
}

function makeMealItemFoodVisible(value) {
    const tar = $('#mealGroup').find($(`option[value='${value}'][hidden]`));
    tar.each(function () {
        this.hidden = false;
        this.disabled = false;
    })
}

function removeMealItemConfirmation(id) {
    const mealItem = mealItems.find(m => m.id === id);
    $('.delete-meal-item-modal').modal('show');
    $('.modal-body').html(`Permanently delete meal item "<b>${mealItem.food.name}</b>"?`);
    $('.modal-footer').html(`
        <a onClick=cancelRemoveMealItem() class="btn btn-secondary mx-2">Cancel</a>
        <a onClick=removeMealItem(${id}) class="btn btn-danger mx-2">Delete</a>`);
}

function cancelRemoveMealItem() {
    $('.delete-meal-item-modal').modal('hide');
}

function getProducts(query, page = 1) {
    query = query.replace(' ', '%20');

    $.ajax({
        url: `/Guest/Product/GetUSDAProducts?userQuery=${query}&pageNumber=${page}`,
        success: function (data) {
            $("#productView").html(data);
            $(".food-chip").on('click', function (e) {
                const foodName = $(this).find('.food-chip-name')[0].innerText
                activateMealModal(foodName)
            });

            const query = $('#searchQuery').val();
            const page = $('#productResultsHeader').attr('page');
            const pageCount = $('#productResultsHeader').attr('pageCount');

            const paginationDiv = getPaginationDiv(query, page, pageCount);
            $('.paginationDiv').html(paginationDiv)
        }
    })
}




function addTemplateListener() {
    $('.template-dropdown').each(function () {
        $(this).on('click', function () {

            const templateId = $(this).val();
            const dateTime = $('#dateTime').val();
            const mealTime = $('#mealDateTime').val();

            $.ajax({
                url: `/Guest/Meal/GetTemplateMeal?id=${templateId}&dateTime=${dateTime}&mealTime=${mealTime}`,
                success: function (data) {
                    //$("#mealCard").html(data);
                    populateMealCard(data);
                    addTemplateListener();
                }
            })
        })
    });
    templateActionListener();
}

function templateActionListener() {
    const createId = 'templateActionCreate';
    const createTitle = 'Create New Template';
    const createColor = 'success';
    const createAction = 'createMealTemplate';

    const dropdownOptions = [
        { id: 'templateActionSave', title: 'Update', color: 'primary', action: 'upsertMealTemplate' },
        { id: 'templateActionUndo', title: 'Undo Changes', color: 'primary', action: 'reloadMealModal' },
        { id: createId, title: createTitle, color: createColor, action: createAction },
        { id: 'templateActionRemove', title: 'Remove', color: 'danger', action: 'removeTemplate' }
    ];

    dropdownOptions.forEach(o => {
        $(`#${o.id}`).on('click', function () {
            updateTemplateActionDisplay(o.title, o.color, o.action, o.hideDropdown);
        })
    });

    $('#createNewSelect').on('click', function () {
        updateTemplateActionDisplay('Add to Templates', createColor, createAction);
        const newDayObj = {
            DateTime: meal.dateTime,
            ActiveMealId: 0,
            Activities: null,
            ActivityIcons: [],
            Day: null,
            IsUserSafeDay: false,
            Meals: [],
            Day: 1,
            Month: 0,
            ReactionIcons: [],
            Reactions: [],
            Year: 0
        }
        getMeal(newDayObj)
    })

    if (meal.isGlobal) {
        upateTemplateActtionButton(createTitle, createColor, createAction);
        $('#templateActionSave').hide();
        $('#templateActionRemove').hide();
        $('#templateActionDivider').hide();
    } 

    disableTemplateActionOnEmptyInput();
}

function updateTemplateActionDisplay(title, color, action, hideDropdown = false) {

    upateTemplateActtionButton(title, color, action);

    if (hideDropdown && !meal.isTemplate) {
        $('#templateActionButton').addClass('add-template-new');
        $('#templateActionDropdownButton').hide();
        $('#templateActionDropdown').hide();

    } else {
        $('#templateActionButton').removeClass('add-template-new');
        $('#templateActionDropdownButton').show();
        $('#templateActionDropdown').show();
    }
}



function upateTemplateActtionButton(title, color, action) {
    $('#templateActionButton')[0].innerText = title;
    $('#templateActionButton')
        .removeClass()
        .addClass(`btn btn-outline-${color}`)
        .removeAttr('onclick')
        .attr('onClick', `${action}(this);`);

    $('#templateActionDropdownButton')
        .removeClass('btn-outline-primary btn-outline-danger btn-outline-success')
        .addClass(`btn-outline-${color}`);
}



function reloadMealModal() {

    const templateId = $('#mealId').val()
    const dateTime = $('#dateTime').val();
    const mealTime = $('#mealDateTime').val();

    $.ajax({
        url: `/Guest/Meal/GetTemplateMeal?id=${templateId}&dateTime=${dateTime}&mealTime=${mealTime}`,
        success: function (data) {
            populateMealCard(data);
            addTemplateListener();
        }
    })
}

function removeTemplate() {
    const mealId = $('#mealId').val();
    const templateId = $('#templateId').val();
    const calendarDate = $('#calendarDate').val();

    $.ajax({
        url: `/Guest/Meal/RemoveMealTemplate?id=${mealId}&dateTime=${calendarDate}`,
        type: 'DELETE',
        contentType: 'application/json',
        success: function (data) {
            if (data) {
                populateMealCard(data);
                addTemplateListener();
            }
        }
    })
}

function disableTemplateActionOnEmptyInput() {

    const mealInput = $('#mealName');

    if (mealInput.val().trim().length == 0) {
        disableAllActions();
    }

    $('#mealTypeInput').on('change', function () {
        toggleTemplateActionButton();
    })

    mealInput.on('keyup', function (e) {
        toggleTemplateActionButton();
    })
}

function toggleTemplateActionButton() {
    const phrase = $('#mealName')
        .val()
        .trim();

    const mealType = $('#mealTypeInput')
        .find('option:selected')
        .val();

    if (phrase.length > 0 && mealType.length > 0) {
        $('#templateActionButton').removeAttr('disabled');
        $('#templateActionDropdownButton').removeAttr('disabled');
    } else {
        disableAllActions();
    }
}

function disableAllActions() {
    $('#templateActionButton').attr('disabled', 'disabled');
    $('#templateActionDropdownButton').attr('disabled', 'disabled');
}

function createMealTemplate() {
    upsertMealTemplate(true);
}

function upsertMealTemplate(forceNewId = false) {
    console.log("Clicked upsert...")
    const Reactions = {};
    $('.reaction-option')
        .each(function () {
            const isChecked = $(this).find('input:checkbox').is(':checked');

            if (isChecked) {
                const id = $(this).val();
                Reactions[id] = true;
            }
        })

    const MealItems = {};

    $('#mealGroup')
        .children()
        .each(function (i) {
            MealItems[i] = {
                Id: forceNewId ? 0 : $(this).find('.mi-id').val(),
                //Id: 0, // keep as zero to create new item so as to avoid conflict with "remove template"
                MealId: forceNewId ? 0 : $(this).find('.mi-mealId').val(),
                FoodId: $(this).find('.mi-food').val(),
                Volume: $(this).find('.mi-volume').val(),
                VolumeUnitsId: $(this).find('.mi-units').val()
            }
        })

    const response = {
        Meal: {
            Id: forceNewId ? 0 : $('#mealId').val(),
            Name: $('#mealName').val(),
            MealTypeId: $('#mealTypeInput').val(),
            ColorId: $('#mealColor').val(),
            DateTime: $('#dateTime').val()
        },
        Reactions,
        MealItems
    }

    $.ajax({
        url: `/Guest/Meal/UpsertMealTemplate`,
        type: 'POST',
        data: JSON.stringify(response),
        contentType: 'application/json',
        success: function (data) {
            if (data) {
                populateMealCard(data);
                addTemplateListener()
            }
        }
    })
}

