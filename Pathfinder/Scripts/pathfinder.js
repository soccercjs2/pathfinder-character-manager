function SingleD20Roll(name, value) {
    $('#rollTitle').text(name);
    $('#rollBonus').text(value);
    $('#btnRoll').data('value', value);

    UpdateRoller($('#rollResult'), $('#singleD20RollString'), Roll('1d20'), value);
    $('#singleD20Roll').modal('show');
}

function AttackRoller(title, weapon, bonuses, damage, critical) {
    $('#attackerTitle').text(title);
    FillAttackTable($('#tblAttacks')[0], weapon, bonuses, damage, critical);
    $('#attackRoller').modal('show');
}

function FillAttackTable(table, weapon, bonuses, damage, critical) {
    bonuses = bonuses.replace(/\+/g, '');
    var array = bonuses.split('/');

    while (table.rows[0]) table.deleteRow(0);
    for (i = 0; i < array.length; i++) {
        AddRow(table, weapon, array[i], damage, critical);
    }
}

function AddRow(table, weapon, bonus, damage, critical) {
    var row = table.insertRow(table.rows.length);
    var weaponCell = row.insertCell(row.cells.length);
    var attackCell = row.insertCell(row.cells.length);
    var damageCell = row.insertCell(row.cells.length);

    weaponCell.innerHTML = weapon;
    attackCell.innerHTML = '<button class="btn btn-default attack-roller-button" data-type="attack" data-equation="1d20 + ' + bonus + '" data-critical="' + critical + '"><i class="sprite sprite-attack"></i></button>';
    damageCell.innerHTML = '<button class="btn btn-default attack-roller-button" data-type="damage" data-equation="' + damage + '"><i class="sprite sprite-damage"></i></button>';
    attackCell.setAttribute('class', 'min');
    damageCell.setAttribute('class', 'min');
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

function Roll(roll)
{
    roll = roll.split('d');
    var result = 0
    var count = roll[0];
    var sides = roll[1];

    for (j = 0; j < count; j++) {
        result += Math.floor((Math.random() * sides) + 1);
    }

    return result;
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

