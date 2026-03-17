const API = '/api/Habits';

// Hämta alla habits när sidan laddas
document.addEventListener('DOMContentLoaded', fetchHabits);

async function fetchHabits() {
    try {
        const res = await fetch(API);
        if (!res.ok) throw new Error('Kunde inte hämta vanor');
        const habits = await res.json();
        renderHabits(habits);
    } catch (err) {
        showFeedback(err.message, 'error');
    }
}

function renderHabits(habits) {
    const list = document.getElementById('habits-list');

    if (habits.length === 0) {
        list.innerHTML = '<p class="empty">Inga vanor än. Lägg till din första! 👆</p>';
        return;
    }

    list.innerHTML = habits.map(h => `
        <div class="habit-card ${!h.isActive ? 'inactive' : ''}">
            <div class="habit-info">
                <h3>${h.name}</h3>
                <p>${h.description || 'Ingen beskrivning'}</p>
                <p>${h.frequencyPerWeek} dagar/vecka</p>
                <p class="streak">🔥 Streak: ${h.currentStreak} dagar</p>
                ${h.lastCompletedAt ? `<p>Senast klar: ${new Date(h.lastCompletedAt).toLocaleDateString('sv-SE')}</p>` : ''}
            </div>
            <div class="habit-actions">
                <button class="btn-complete" onclick="completeHabit(${h.id})">✅ Klar</button>
                <button class="btn-edit" onclick="editHabit(${h.id}, '${h.name}', '${h.description}', ${h.frequencyPerWeek}, ${h.isActive})">✏️ Redigera</button>
                <button class="btn-delete" onclick="deleteHabit(${h.id})">🗑️ Ta bort</button>
            </div>
        </div>
    `).join('');
}

async function submitForm() {
    const id = document.getElementById('edit-id').value;
    const name = document.getElementById('name').value.trim();
    const description = document.getElementById('description').value.trim();
    const frequency = parseInt(document.getElementById('frequency').value);

    // Frontend-validering
    const nameRegex = /^[a-zA-ZåäöÅÄÖ\s]+$/;
    
    if (!name) {
        showFeedback('Namn är obligatoriskt!', 'error');
        return;
    }
    if (!nameRegex.test(name)) {
        showFeedback('Namn får bara innehålla bokstäver!', 'error');
        return;
    }
    if (name.length < 3) {
        showFeedback('Namn måste vara minst 3 tecken!', 'error');
        return;
    }
    if (description && !nameRegex.test(description)) {
        showFeedback('Beskrivning får bara innehålla bokstäver!', 'error');
        return;
    }
    if (!frequency || frequency < 1 || frequency > 7) {
        showFeedback('Frekvens måste vara mellan 1 och 7!', 'error');
        return;
    }

    try {
        let res;
        if (id) {
            res = await fetch(`${API}/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ name, description, frequencyPerWeek: frequency, isActive: true })
            });
        } else {
            res = await fetch(API, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ name, description, frequencyPerWeek: frequency })
            });
        }

        if (!res.ok) {
            const errorData = await res.json();
            const errors = Object.values(errorData.errors || {}).flat();
            showFeedback(errors.length > 0 ? errors[0] : 'Något gick fel!', 'error');
            return;
        }

        showFeedback(id ? 'Vanan uppdaterades! ✅' : 'Ny vana skapades! 🌱', 'success');
        clearForm();
        fetchHabits();
    } catch (err) {
        showFeedback('Kunde inte ansluta till servern!', 'error');
    }
}

async function deleteHabit(id) {
    if (!confirm('Är du säker på att du vill ta bort denna vana?')) return;

    try {
        const res = await fetch(`${API}/${id}`, { method: 'DELETE' });
        if (!res.ok) throw new Error('Kunde inte ta bort vanan');
        showFeedback('Vanan togs bort! 🗑️', 'success');
        fetchHabits();
    } catch (err) {
        showFeedback(err.message, 'error');
    }
}

async function completeHabit(id) {
    try {
        const res = await fetch(`${API}/${id}/complete`, { method: 'POST' });
        if (!res.ok) throw new Error('Kunde inte markera vanan som klar');
        showFeedback('Bra jobbat! Streak ökad! 🔥', 'success');
        fetchHabits();
    } catch (err) {
        showFeedback(err.message, 'error');
    }
}

function editHabit(id, name, description, frequency, isActive) {
    document.getElementById('edit-id').value = id;
    document.getElementById('name').value = name;
    document.getElementById('description').value = description;
    document.getElementById('frequency').value = frequency;
    document.getElementById('form-title').textContent = 'Redigera vana';
    document.getElementById('submit-btn').textContent = 'Spara ändringar';
    document.getElementById('cancel-btn').style.display = 'block';
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

function cancelEdit() {
    clearForm();
}

function clearForm() {
    document.getElementById('edit-id').value = '';
    document.getElementById('name').value = '';
    document.getElementById('description').value = '';
    document.getElementById('frequency').value = '';
    document.getElementById('form-title').textContent = 'Lägg till ny vana';
    document.getElementById('submit-btn').textContent = 'Lägg till';
    document.getElementById('cancel-btn').style.display = 'none';
}

function showFeedback(message, type) {
    const feedback = document.getElementById('feedback');
    feedback.textContent = message;
    feedback.className = `feedback ${type}`;
    feedback.style.display = 'block';
    setTimeout(() => { feedback.style.display = 'none'; }, 3000);
}