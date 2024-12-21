INSERT INTO Events([Id], [Name], [Date], [Location])
VALUES
('75eff421-2e16-43ff-8f56-b754ad991074', '.NET Conference Milano 2024', '2024-12-16', 'Microsoft House, Milano'),
('1ca8f1a7-5d96-4b57-a879-5e541abb79d2', 'Web day 2025', '2025-03-31', 'TAG Calabiana, Milano'),
('8647a740-81f5-4029-9d82-396a1051dff2', 'An online event about .NET', '2025-02-13', 'Online')

INSERT INTO Talks([Id], [Title], [Abstract])
VALUES
(
    '5a59a541-189e-4935-b31a-1b11c46e2055', 
    'Blazor tutti i gusti più uno', 
    'In questa sessione vedremo insieme come Blazor possa essere utilizzato al di fuori del mondo Web'
),
(
    'b8854764-f534-49c3-adb8-c42df1d8789a',
    'Da SSR a WebAssembly: ecco Blazor 8',
    'Con .NET 8 Blazor si è evoluto in modo da essere uno strumento completo per le nostre applicazioni web. Vediamo insieme quali sono le funzionalità chiave'
)