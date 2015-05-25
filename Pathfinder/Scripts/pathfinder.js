﻿function GoToRoller(name, value) {
    //var url = '/Character/Roller/' + name + '/\'1d20\'p' + value;

    var result = Math.floor((Math.random() * 20) + 1) + value;

    $('#rollTitle').text(name);
    $('#rollBonus').text(value);
    $('#btnRoll').data('value', value);
    $('#rollResult').text(Roll(value));
    $('#singleD20Roll').modal('show');

    //window.open(url, '_blank', 'width=400,height=140,resizable=1');
}

function Roll(value)
{
    return Math.floor((Math.random() * 20) + 1) + value;
}

function ToggleVision(id, visibleStyle) {
    var item = document.getElementById(id);
    if (item.currentStyle.display == 'none') {
        item.style.display = visibleStyle;
    }
    else {
        item.style.display = 'none';
    }
};

function ToggleRowsVisible(name) {
    var table = document.getElementById(name);
    for (i = 1; i < table.rows.length; i++) {
        var row = table.rows[i];
        if (row.style.display == 'none') {
            row.style.display = 'table-row';
        }
        else {
            row.style.display = 'none';
        }
    }

    var container = document.querySelector('#divAbilities');
    var msnry = new Masonry(container, {
        // options
        columnWidth: 285,
        itemSelector: '.masonry-box'
    });
};

function UpdateAbility(abilityId, characterId, abilityTypeId, name, description, isConditional, active) {
    var ability = {
        AbilityId: abilityId,
        CharacterId: characterId,
        AbilityTypeId: abilityTypeId,
        Name: name,
        Description: description,
        IsConditional: isConditional,
        Active: active
    }

    $.ajax({
        type: 'POST',
        dataType: 'text',
        url: "/Character/UpdateAbility",
        data: ability,
        dataType: 'json',
        //success: function (response) {
        //    window.location.href = response.Url;
        //}
    });
};

