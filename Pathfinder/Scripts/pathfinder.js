﻿function GoToRoller(name, value) {
    $('#rollTitle').text(name);
    $('#rollBonus').text(value);
    $('#btnRoll').data('value', value);

    //var roll = Roll();
    //if (roll == 20) { $('#rollResult').addClass('critical'); } else { $('#rollResult').removeClass('critical'); }
    //$('#rollResult').text(roll + value);
    //$('#rollString').text(RollString(roll, value));
    UpdateRoller($('#rollResult'), $('#rollString'), Roll(), value);
    $('#singleD20Roll').modal('show');
}

function UpdateRoller(lblResult, lblMath, roll, value) {
    if (roll == 20) {
        lblResult.removeClass('critical-failure');
        lblResult.addClass('critical');
    }
    else if (roll == 1) {
        lblResult.removeClass('critical');
        lblResult.addClass('critical-failure');
    }
    else {
        lblResult.removeClass('critical');
        lblResult.removeClass('critical-failure');
    }

    lblResult.text(roll + value);
    lblMath.text(RollString(roll, value));

}

function Roll()
{
    return Math.floor((Math.random() * 20) + 1);
}

function RollString(roll, value)
{
    var operator = '+';
    if (value < 0) { operator = '-' }

    var rollString = '1d20 ' + operator + ' ' + Math.abs(value);
    rollString += ' = (' + roll + ') ' + operator + ' ' + Math.abs(value);
    rollString += ' = ' + (roll + value);
    return rollString;
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

