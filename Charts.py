import matplotlib.pyplot as plt
import numpy as np
import re
import os

def parse_data(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        for _ in range(2):
            f.readline()
        
        headers = f.readline().strip().split('\t')
        headers = [h.strip() for h in headers if h.strip()]

        rows = []
        for line in f:
            if not line.strip():
                continue
            parts = re.split(r'\t+', line.strip())
            row_name = parts[0]
            values = []
            for v in parts[1:]:
                if v.strip().upper() in ['N/A', 'INF', 'INF (SOF)']:
                    values.append(np.nan)
                else:
                    v = v.replace(',', '.')
                    try:
                        values.append(float(v))
                    except ValueError:
                        values.append(np.nan)
            rows.append((row_name, values))
    
    return headers, rows

def plot_data(headers, rows):
    algorithms = headers[1:]
    
    for row_name, values in rows:
        plt.figure(figsize=(12, 6))
        bars = plt.bar(algorithms, values)
        
        for bar in bars:
            height = bar.get_height()
            if not np.isnan(height):
                plt.text(bar.get_x() + bar.get_width()/2., height,
                         f'{height:.4f}',
                         ha='center', va='bottom', rotation=45, fontsize=8)
        
        plt.title(f'Сравнение алгоритмов сортировки: {row_name}')
        plt.ylabel('Время (мс)')
        plt.xticks(rotation=45)
        plt.tight_layout()
        
        filename = re.sub(r'[^\w]', '_', row_name) + '.png'
        output_path = os.path.join("size_500000", filename)
        plt.savefig(output_path, dpi=300, bbox_inches='tight')
        plt.close()
        print(f'График сохранен как: {filename}')

if __name__ == '__main__':
    input_file = 'SortResults_Size_500000.txt'
    
    try:
        headers, rows = parse_data(input_file)
        plot_data(headers, rows)
        print("Все графики успешно построены!")
    except FileNotFoundError:
        print(f"Ошибка: файл '{input_file}' не найден.")
    except Exception as e:
        print(f"Произошла ошибка: {str(e)}")