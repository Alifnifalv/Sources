# ports.py
import socket

def is_port_available(host, port, timeout=1):
    """

    Args:
        host (str): The address of the computer to check (like the mall address).
        port (int): The port number to check (like the parking spot number).
        timeout (int, optional): How long to wait before giving up checking (in seconds). Defaults to 1 second.

    Returns:
        bool: True if the port seems available, False if it's in use or if there was an error checking.
    """
    try:
        with socket.create_connection((host, port), timeout=timeout):
            return False  # Port is open/in use
    except (ConnectionRefusedError, socket.timeout):
        return True  # Port is closed/available
    except Exception as e:
        print(f"Error checking {host}:{port} - {str(e)}")
        return False

def is_range_fully_available(host, port_range):
    """

    Args:
        host (str): The address of the computer to check.
        port_range (range): A range of port numbers to check (e.g., range(940, 951) for ports 940 to 950).

    Returns:
        bool: True if all ports in the range are available, False if any port is in use.
    """
    return all(is_port_available(host, port) for port in port_range)

def assign_port(host, port_ranges, assigned_ports):
    """

    Args:
        host (str): The address of the computer to check for port availability.
        port_ranges (list of range): A list of port ranges to check for availability.
        assigned_ports (list of int): A list of ports that are already assigned and should not be used again.

    Returns:
        int or None: An available port number if found, otherwise None.
    """
    for port_range in port_ranges:
        if is_range_fully_available(host, port_range):
            for port in port_range:
                if port not in assigned_ports:
                    assigned_ports.append(port)
                    return port
    return None

def get_dynamic_port_from_ranges(host, list_of_ranges, assigned_ports, range_width=10, max_port=65535):
    """
    Args:
        host (str): The computer to check ports on.
        list_of_ranges (list of range): Initial list of preferred port ranges.
        assigned_ports (list of int): Ports already in use.
        range_width (int, optional): Size of each port range to check. Defaults to 10.
        max_port (int, optional): Highest port number to consider. Defaults to 65535.

    Returns:
        int: An available port number.

    Raises:
        ValueError: If no available port is found within the given ranges and up to max_port.
    """
    # Static variables to maintain state across function calls - like remembering where we last found free parking
    if not hasattr(get_dynamic_port_from_ranges, 'current_range'):
        get_dynamic_port_from_ranges.current_range = None
        get_dynamic_port_from_ranges.available_ports = []

    def check_range_availability(range_to_check):
        
        print(f"Checking range {range_to_check.start}-{range_to_check.stop-1}")
        available = []

        for port in range_to_check:
            if not is_port_available(host, port):
                print(f"Port {port} is unavailable - skipping entire range")
                return None
            available.append(port)

        return available

    # If we have cached available ports, use them first - like using spots we already know are free
    if get_dynamic_port_from_ranges.available_ports:
        port = get_dynamic_port_from_ranges.available_ports[0]
        if port not in assigned_ports:
            assigned_ports.append(port)
            get_dynamic_port_from_ranges.available_ports.pop(0)
            print(f"Found available port {port} in dynamic range")
            return port

    # If no cached ports, search for a new valid range - need to look for a new block of parking spots
    current_start = (max(assigned_ports) if assigned_ports else 900)
    current_start = (current_start // range_width) * range_width  # Align to range boundary - start checking from the beginning of a block

    while current_start <= max_port:
        current_end = min(current_start + range_width - 1, max_port)
        current_range = range(current_start, current_end + 1)

        available_ports = check_range_availability(current_range)
        if available_ports:
            # Cache the available ports for future use - remember all spots in this free block
            get_dynamic_port_from_ranges.available_ports = available_ports
            get_dynamic_port_from_ranges.current_range = current_range

            # Use the first available port - take the first spot in the free block
            port = get_dynamic_port_from_ranges.available_ports.pop(0)
            assigned_ports.append(port)
            print(f"Found available port {port} in dynamic range")
            return port

        # Move to the start of the next range - check the next block of parking spots
        current_start = current_end + 1

    raise ValueError(f"No fully available port ranges found up to {max_port}")

def parse_range(range_str):
    """
    Args:
        range_str (str): A string representing a range of ports in the format "start-end" (e.g., "940-950").

    Returns:
        range: A range object representing the parsed port range.

    Raises:
        ValueError: If the input string is not in the correct format or if the port numbers are invalid.
    """
    start, end = map(int, range_str.split("-"))
    if not (0 <= start <= 65535) or not (0 <= end <= 65535):
        raise ValueError("Ports must be between 0 and 65535")
    return range(start, end + 1)
